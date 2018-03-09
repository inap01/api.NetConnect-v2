using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Catering.Backend;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class CateringProductAttributeRelationDataController : BaseDataController, IDataController<CateringProductAttributeRelation>
    {
        public CateringProductAttributeRelationDataController() : base()
        {

        }

        #region Basic Functions
        public CateringProductAttributeRelation GetItem(int ID)
        {
            var qry = db.CateringProductAttributeRelation.AsQueryable();

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<CateringProductAttributeRelation> GetItems()
        {
            var qry = db.CateringProductAttributeRelation.AsQueryable();

            return qry;
        }

        public CateringProductAttributeRelation Insert(CateringProductAttributeRelation item)
        {
            var result = db.CateringProductAttributeRelation.Add(item);
            db.SaveChanges();

            return result;
        }

        public CateringProductAttributeRelation Update(CateringProductAttributeRelation item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            db.CateringProductAttributeRelation.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion

        public List<BackendProductAttributeViewModelItem> UpdateProduct(CateringProduct model, List<BackendProductAttributeViewModelItem> AttributeList)
        {
            var entries = GetItems().Where(x => x.CateringProductID == model.ID).ToList();

            // Suche nach Einträgen die gelöscht werden sollen
            foreach(var entry in entries)
            {
                if(AttributeList.FirstOrDefault(x => x.ID == entry.CateringProductAttributeID) == null)
                {
                    // Eintrag ist nicht mehr vorhanden -> Löschen
                    Delete(entry.ID);
                }
            }

            // Suche nach Einträgen die Neu sind
            foreach (var attr in AttributeList)
            {
                if (entries.FirstOrDefault(x => x.CateringProductAttributeID == attr.ID) == null)
                {
                    // Eintrag noch nicht vorhanden -> Hinzufügen
                    Insert(new CateringProductAttributeRelation() { CateringProductID = model.ID, CateringProductAttributeID = attr.ID });
                }
            }

            return AttributeList;
        }
    }
}