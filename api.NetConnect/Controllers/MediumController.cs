using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;

namespace api.NetConnect.Controllers
{
    [Authorize()]
    public class MediumController : ApiController
    {
        String uploadDir = System.Web.HttpContext.Current.Server.MapPath(Properties.Settings.Default.imageTmpUpload);

        [HttpPost]
        public async Task<IHttpActionResult> Upload()
        {
            // ensure that the request contains multipart/form-data
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);
            MultipartFormDataStreamProvider provider =
                new MultipartFormDataStreamProvider(uploadDir);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                int nChunkNumber = Convert.ToInt32(provider.FormData["flowChunkNumber"]);
                int nTotalChunks = Convert.ToInt32(provider.FormData["flowTotalChunks"]);
                string sIdentifier = provider.FormData["flowIdentifier"];
                string sFileName = DateTime.Now.Ticks + "_" + provider.FormData["flowFilename"];
                sFileName = sFileName.Replace(Path.GetExtension(sFileName), ".jpg");

                // rename the generated file
                MultipartFileData chunk = provider.FileData[0]; // Only one file in multipart message
                RenameChunk(chunk, nChunkNumber, sIdentifier);

                // assemble chunks into single file if they're all here
                TryAssembleFile(sIdentifier, nTotalChunks, sFileName);

                return Ok(sFileName);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private string GetChunkFileName(int chunkNumber, string identifier)
        {
            return Path.Combine(uploadDir,
                String.Format(CultureInfo.InvariantCulture, "{0}_{1}",
                    identifier, chunkNumber));
        }

        private void RenameChunk(MultipartFileData chunk, int chunkNumber, string identifier)
        {
            string sGeneratedFileName = chunk.LocalFileName;
            string sChunkFileName = GetChunkFileName(chunkNumber, identifier);
            if (File.Exists(sChunkFileName)) File.Delete(sChunkFileName);
            File.Move(sGeneratedFileName, sChunkFileName);
        }

        private string GetFileName(string identifier)
        {
            return Path.Combine(uploadDir, identifier);
        }

        private bool IsChunkHere(int chunkNumber, string identifier)
        {
            string sFileName = GetChunkFileName(chunkNumber, identifier);
            return File.Exists(sFileName);
        }

        private bool AreAllChunksHere(string identifier, int totalChunks)
        {
            for (int nChunkNumber = 1; nChunkNumber <= totalChunks; nChunkNumber++)
                if (!IsChunkHere(nChunkNumber, identifier)) return false;
            return true;
        }

        private void TryAssembleFile(string identifier, int totalChunks, string filename)
        {
            if (!AreAllChunksHere(identifier, totalChunks)) return;

            // create a single file
            string sConsolidatedFileName = GetFileName(identifier);
            using (Stream destStream = File.Create(sConsolidatedFileName, 15000))
            {
                for (int nChunkNumber = 1; nChunkNumber <= totalChunks; nChunkNumber++)
                {
                    string sChunkFileName = GetChunkFileName(nChunkNumber, identifier);
                    using (Stream sourceStream = File.OpenRead(sChunkFileName))
                    {
                        sourceStream.CopyTo(destStream);
                    }
                } //efor
                destStream.Close();
            }

            // rename consolidated with original name of upload
            // strip to filename if directory is specified (avoid cross-directory attack)
            filename = Path.GetFileName(filename);
            Debug.Assert(filename != null);

            string sRealFileName = Path.Combine(uploadDir, filename);
            if (File.Exists(filename)) File.Delete(sRealFileName);
            File.Move(sConsolidatedFileName, sRealFileName);

            // delete chunk files
            for (int nChunkNumber = 1; nChunkNumber <= totalChunks; nChunkNumber++)
            {
                string sChunkFileName = GetChunkFileName(nChunkNumber, identifier);
                File.Delete(sChunkFileName);
            } //efor
        }
    }
}