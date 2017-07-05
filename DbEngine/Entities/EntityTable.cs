using DBEngineProject.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;
using System.Diagnostics;

namespace DBEngineProject.Entities
{

    [DebuggerDisplay("SqlText = {SqlText}")]
    public class EntityTable: IEnumerator<EntityRow>, IEnumerable<EntityRow>
    {

        #region Properties: Private

        private int _currentIndex { get; set; }

        #endregion

        #region Properties: Public

        public string SqlText { get; set; }

        public int Count { get { return EntityRows.Count; } }

        public readonly IDataReader DataReader;

        private List<EntityRow> _entityRows;
        public List<EntityRow> EntityRows
        {
            get { return _entityRows ?? (_entityRows = new List<EntityRow>()); }
            protected set
            {
                value.CheckNull(nameof(EntityRows));
                _entityRows = value;
            }
        }

        public EntityRow Current => this[_currentIndex == -1 ? 0 : _currentIndex];

        object IEnumerator.Current => this[_currentIndex == -1 ? 0 : _currentIndex];

        public EntityRow this[int index]
        {
            get { return EntityRows[index]; }
        }

        #endregion

        #region Constructors: Protected

        protected EntityTable()
        {
            _currentIndex = -1;
        }

        protected EntityTable(string sqlText)
            :this()
        {
            SqlText = sqlText;
        }

        #endregion

        #region Cosntructors: Public

        public EntityTable(SqlDataReader reader)
            :this(reader, "") { }

        public EntityTable(SqlDataReader reader, string sqlText)
            :this(sqlText)
        {
            reader.CheckNull(nameof(reader));
            if (reader.IsClosed)
                throw new ArgumentException("Sql reader can`t be closed.");
            DataReader = reader;
            EntityRowSchema rowSchema = new EntityRowSchema(reader);
            while (reader.Read())
                EntityRows.Add(new EntityRow(reader, rowSchema));
        }

        public EntityTable(IEnumerable<EntityRow> rows)
            :this(rows, "")
        {
            
        }

        public EntityTable(IEnumerable<EntityRow> rows, string sqlText)
            :this(sqlText)
        {
            EntityRows = rows.ToList();
        }

        #endregion

        #region Methods: Public

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            _currentIndex++;
            if(_currentIndex < Count)
            {
                return true;
            }
            Reset();
            return false;
        }

        public void Reset()
        {
            _currentIndex = -1;
        }

        public IEnumerator<EntityRow> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public virtual List<T> GetModels<T>()
            where T : Entity, new()
        {
            return EntityRows.Select(x => 
            {
                var model = new T();
                model.Init(x);
                return model;
            }).ToList();
        }

        #endregion

    }
}
