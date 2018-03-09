using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel.Partner;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace api.NetConnect.DataControllers
{
    public class PartnerDisplayRelationDataController : BaseDataController, IDataController<PartnerDisplayRelation>
    {
        public PartnerDisplayRelationDataController() : base()
        {

        }

        #region Basic Functions
        public PartnerDisplayRelation GetItem(int ID)
        {
            var qry = db.PartnerDisplayRelation.AsQueryable();
            qry.Include(x => x.Partner);
            qry.Include(x => x.PartnerDisplay);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<PartnerDisplayRelation> GetItems()
        {
            var qry = db.PartnerDisplayRelation.AsQueryable();
            qry.Include(x => x.Partner);
            qry.Include(x => x.PartnerDisplay);

            return qry;
        }

        public PartnerDisplayRelation Insert(PartnerDisplayRelation item)
        {
            var result = db.PartnerDisplayRelation.Add(item);
            db.SaveChanges();

            return result;
        }

        public PartnerDisplayRelation Update(PartnerDisplayRelation item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            db.PartnerDisplayRelation.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion

        public List<data.ViewModel.Partner.PartnerDisplay> UpdatePartner(Partner model, List<data.ViewModel.Partner.PartnerDisplay> Displays)
        {
            var entries = GetItems().Where(x => x.PartnerID == model.ID);

            foreach(var display in Displays)
            {
                // Display setzen aber Eintrag existiert noch nicht
                if(display.Value && entries.SingleOrDefault(x => x.PartnerDisplayID == display.ID) == null)
                {
                    // Hinzufügen
                    Insert(new PartnerDisplayRelation() { PartnerID = model.ID, PartnerDisplayID = display.ID });
                }
                // Display setzen und ein Eintrag ist vorhanden
                else if (display.Value && entries.SingleOrDefault(x => x.PartnerDisplayID == display.ID) != null)
                {
                    // Do nothing
                }
                // Display NICHT setzen und ein Eintrag ist vorhanden
                else if (!display.Value && entries.SingleOrDefault(x => x.PartnerDisplayID == display.ID) != null)
                {
                    // Entfernen
                    Delete(GetItems().Single(x => x.PartnerID == model.ID && x.PartnerDisplayID == display.ID).ID);
                }
                // Display NICHT setzen aber Eintrag existiert noch nicht
                else if (!display.Value && entries.SingleOrDefault(x => x.PartnerDisplayID == display.ID) == null)
                {
                    // Do nothing
                }
            }

            return Displays;
        }
    }
}