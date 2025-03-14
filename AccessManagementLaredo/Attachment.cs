using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;
using DataTier;


// *********************************************************************************************
//      CHECK THIS CLASS!!!!!!!!!!!!!!!
// *********************************************************************************************

namespace AccessManagementLaredo
{
    // *********************************************************************************************
    //                                  Specific Interface.
    // *********************************************************************************************
    public interface IAttachmentRepository : Interfaces.IRepository<Attachment>
    {
    }

    // *********************************************************************************************
    //                                 Basic Structure Class.
    // *********************************************************************************************

    public class Attachment
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int AttachmentTypeId { get; set; }
        public int PermitRequestId { get; set; }
    }

    // *********************************************************************************************
    //                                  Repository Class.
    // *********************************************************************************************
    public class AttachmentRepository : IAttachmentRepository
    {
        private Interfaces.IUnitOfWork _unitOfWork;
        private StringBuilder _strQuery = new StringBuilder();
        private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

        // ---------------------------------------------------------------------------------------------
        //                  Constructor.
        // ---------------------------------------------------------------------------------------------
        public AttachmentRepository(Interfaces.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Insert a new record.
        // ---------------------------------------------------------------------------------------------
        public int Create(Attachment entity)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("INSERT INTO ATCHMT (");
            _strQuery.Append("ATCHMT_NM, ");
            _strQuery.Append("ATCHMT_TYPE_ID, ");
            _strQuery.Append("PRMT_RQST_ID");
            _strQuery.Append(") OUTPUT inserted.ATCHMT_ID ");
            _strQuery.Append("VALUES (");
            _strQuery.Append("@prm_name, ");
            _strQuery.Append("@prm_attachment_type, ");
            _strQuery.Append("@prm_permit_request");
            _strQuery.Append(")");

            _queryParams.Clear();
            _queryParams.Add("prm_name", entity.Name);
            _queryParams.Add("prm_attachment_type", entity.AttachmentTypeId);
            _queryParams.Add("prm_permit_request", entity.PermitRequestId);

            int sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);

            return sequenceValue;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Delete record.
        // ---------------------------------------------------------------------------------------------
        public void Delete(int id)
        {
            _strQuery.Clear();
            _strQuery.Append("DELETE FROM ATCHMT WHERE ");
            _strQuery.Append("ATCHMT_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Update record.
        // ---------------------------------------------------------------------------------------------
        public void Update(Attachment entity, int id)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("UPDATE ATCHMT SET ");
            _strQuery.Append("ATCHMT_NM = @prm_name, ");
            _strQuery.Append("ATCHMT_TYPE_ID = @prm_attachment_type, ");
            _strQuery.Append("PRMT_RQST_ID = @prm_permit_request ");
            _strQuery.Append("WHERE ");
            _strQuery.Append("ATCHMT_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);
            _queryParams.Add("prm_name", entity.Name);
            _queryParams.Add("prm_attachment_type", entity.AttachmentTypeId);
            _queryParams.Add("prm_permit_request", entity.PermitRequestId);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Get all records.
        // ---------------------------------------------------------------------------------------------
        public string Read(int? id = -1)
        {
            _strQuery.Clear();

            _strQuery.Append("SELECT ");
            _strQuery.Append("ATCHMT_ID AS Id, ");
            _strQuery.Append("ATCHMT_NM AS Name, ");
            _strQuery.Append("ATCHMT_TYPE_ID AS AttachmentTypeId, ");
            _strQuery.Append("PRMT_RQST_ID AS PermitRequestId ");
            _strQuery.Append("FROM ATCHMT ");

            // A record with specific "id" is searched.
            if (id != -1)
            {
                _strQuery.Append("WHERE ");
                _strQuery.Append("ATCHMT_ID = @prm_id ");
            }

            _strQuery.Append("ORDER BY ");
            _strQuery.Append("ATCHMT_NM ");
            _strQuery.Append("FOR JSON AUTO");

            // A record with specific "id" is searched.
            _queryParams.Clear();
            if (id != -1)
            {
                _queryParams.Add("prm_id", id);
            }

            string result = _unitOfWork.GetRecords(_strQuery.ToString(), _queryParams);

            return result;
        }

        // ---------------------------------------------------------------------------------------------
        //        Release database resources.       
        // ---------------------------------------------------------------------------------------------
        public void DisposeDBObjects()
        {
            _unitOfWork.ReleaseDBObjects();
        }

        // ---------------------------------------------------------------------------------------------
        //               Convert to upper case specific fields before CRUD operation.
        // ---------------------------------------------------------------------------------------------
        private static void ConvertCase(Attachment entity)
        {
            entity.Name = (entity.Name != null) ? entity.Name.ToUpper() : "";
            //entity.Description = (entity.Description != null) ? entity.Description.ToUpper() : "";
        }
    }
}

