using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DataTier;


namespace AccessManagementLaredo
{
    // *********************************************************************************************
    //                                  Specific Interface.
    // *********************************************************************************************
    public interface IAreaOfficeRepository : Interfaces.IRepository<AreaOffice>
    {
    }

    // *********************************************************************************************
    //                                 Basic Structure Class.
    // *********************************************************************************************

    public class AreaOffice
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Number { get; set; }
        public string Description { get; set; }
    }

    // *********************************************************************************************
    //                                  Repository Class.
    // *********************************************************************************************
    public class AreaOfficeRepository : IAreaOfficeRepository
    {
        private Interfaces.IUnitOfWork _unitOfWork;
        private StringBuilder _strQuery = new StringBuilder();
        private Dictionary<string, object> _queryParams = new Dictionary<string, object>();

        // ---------------------------------------------------------------------------------------------
        //                  Constructor.
        // ---------------------------------------------------------------------------------------------
        public AreaOfficeRepository(Interfaces.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Insert a new record.
        // ---------------------------------------------------------------------------------------------
        public int Create(AreaOffice entity)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("INSERT INTO AREA_OFFC (");
            _strQuery.Append("AREA_OFFC_NM, ");
			_strQuery.Append("AREA_OFFC_NBR, ");
			_strQuery.Append("AREA_OFFC_DSCR");
            _strQuery.Append(") OUTPUT inserted.AREA_OFFC_ID ");
            _strQuery.Append("VALUES (");
            _strQuery.Append("@prm_name, ");
			_strQuery.Append("@prm_number, ");
			_strQuery.Append("@prm_description");
            _strQuery.Append(")");

            _queryParams.Clear();
            _queryParams.Add("prm_name", entity.Name);
			_queryParams.Add("prm_number", entity.Number);
			_queryParams.Add("prm_description", entity.Description);

            int sequenceValue = (int)_unitOfWork.ExecuteScalar(_strQuery.ToString(), _queryParams);

            return sequenceValue;
        }

        // ---------------------------------------------------------------------------------------------
        //                  Delete record.
        // ---------------------------------------------------------------------------------------------
        public void Delete(int id)
        {
            _strQuery.Clear();
            _strQuery.Append("DELETE FROM AREA_OFFC WHERE ");
            _strQuery.Append("AREA_OFFC_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Update record.
        // ---------------------------------------------------------------------------------------------
        public void Update(AreaOffice entity, int id)
        {
            ConvertCase(entity);

            _strQuery.Clear();
            _strQuery.Append("UPDATE AREA_OFFC SET ");
            _strQuery.Append("AREA_OFFC_NM = @prm_name, ");
			_strQuery.Append("AREA_OFFC_NBR = @prm_number, ");
			_strQuery.Append("AREA_OFFC_DSCR = @prm_description ");
            _strQuery.Append("WHERE ");
            _strQuery.Append("AREA_OFFC_ID = @prm_id");

            _queryParams.Clear();
            _queryParams.Add("prm_id", id);
            _queryParams.Add("prm_name", entity.Name);
			_queryParams.Add("prm_number", entity.Number);
			_queryParams.Add("prm_description", entity.Description);

            _unitOfWork.ExecuteNonQuery(_strQuery.ToString(), _queryParams);
        }

        // ---------------------------------------------------------------------------------------------
        //                  Get all records.
        // ---------------------------------------------------------------------------------------------
        public string Read(int? id = -1)
        {
            _strQuery.Clear();

            _strQuery.Append("SELECT ");
            _strQuery.Append("AREA_OFFC_ID AS Id, ");
            _strQuery.Append("AREA_OFFC_NM AS Name, ");
			_strQuery.Append("AREA_OFFC_NBR AS Number, ");
			_strQuery.Append("AREA_OFFC_DSCR AS Description ");
            _strQuery.Append("FROM AREA_OFFC ");

            // A record with specific "id" is searched.
            if (id != -1)
            {
                _strQuery.Append("WHERE ");
                _strQuery.Append("AREA_OFFC_ID = @prm_id ");
            }

            _strQuery.Append("ORDER BY ");
            _strQuery.Append("AREA_OFFC_NM ");
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
        private static void ConvertCase(AreaOffice entity)
        {
            entity.Name = (entity.Name != null) ? entity.Name.ToUpper() : DBNull.Value.ToString();
			entity.Number = (entity.Number!= null) ? entity.Number.ToUpper() : DBNull.Value.ToString();
			entity.Description = (entity.Description != null) ? entity.Description.ToUpper() : DBNull.Value.ToString();
		}
    }
}

