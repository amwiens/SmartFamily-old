﻿using SmartFamily.Gedcom.Common;

using System.Collections;
using System.Text;

namespace SmartFamily.Gedcom.Records
{
    public class GEDCOMRecordList : IList<GEDCOMRecord>
    {
        private readonly List<GEDCOMRecord> _list;
        private readonly Dictionary<GEDCOMTag, int> _maxIdDictionary;
        private readonly Dictionary<GEDCOMTag, List<GEDCOMRecord>> _tagDictionary;

        #region Constructors

        public GEDCOMRecordList()
        {
            _list = new List<GEDCOMRecord>();
            _tagDictionary = new Dictionary<GEDCOMTag, List<GEDCOMRecord>>();
            _maxIdDictionary = new Dictionary<GEDCOMTag, int>();
        }

        #endregion Constructors

        #region Private Methods

        private void AddToDictionary(GEDCOMRecord item, bool addToList)
        {
            List<GEDCOMRecord> tagList = null;
            bool bFound = _tagDictionary.TryGetValue(item.TagName, out tagList);

            if (bFound)
            {
                if (addToList)
                {
                    // Add item to List
                    tagList.Add(item);
                }
            }
            else
            {
                // Create new List
                tagList = new List<GEDCOMRecord>();

                // Add item to List
                tagList.Add(item);

                // Add List to Dictionary
                _tagDictionary[item.TagName] = tagList;
            }

            // Update Max Id Dictionary
            if (!string.IsNullOrEmpty(item.GetId()))
            {
                UpdateMaxIdDictionary(item);
            }
        }

        private void RemoveFromDictionary(GEDCOMRecord item)
        {
            List<GEDCOMRecord> tagList = null;
            bool bFound = _tagDictionary.TryGetValue(item.TagName, out tagList);

            if (bFound)
            {
                // Remove item from List
                tagList.Remove(item);

                if (tagList.Count == 0)
                {
                    // remove the empty List from the dictionary
                    _tagDictionary.Remove(item.TagName);
                }
            }
            else
            {
                throw new Exception();
            }
        }

        private bool TryGetRecord(GEDCOMTag tagName, out GEDCOMRecord item)
        {
            List<GEDCOMRecord> tagList = null;
            item = null;

            bool bFound = _tagDictionary.TryGetValue(tagName, out tagList);

            if (bFound)
            {
                if (tagList.Count > 0)
                {
                    item = tagList[0];
                }
                else
                {
                    bFound = false;
                }
            }

            return bFound;
        }

        private void UpdateMaxIdDictionary(GEDCOMRecord item)
        {
            int maxId = -1;
            int id = Int32.Parse(item.GetId());

            if (_maxIdDictionary.TryGetValue(item.TagName, out maxId))
            {
                if (id > maxId)
                {
                    _maxIdDictionary[item.TagName] = id;
                }
            }
            else
            {
                _maxIdDictionary[item.TagName] = id;
            }
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// An indexer to access the GEDCOMRecords using the xREfId of The <see cref="GEDCOMRecord"/>.
        /// </summary>
        public GEDCOMRecord this[string id]
        {
            get => this[IndexOf(id)];
            set
            {
                int index = IndexOf(id);
                this[index] = value;
            }
        }

        /// <summary>
        /// Adds a list of records to the collection.
        /// </summary>
        /// <param name="records">A <see cref="GEDCOMRecordList"/>.</param>
        public void AddRange(GEDCOMRecordList records)
        {
            for (int i = 0; i < records.Count; i++)
            {
                GEDCOMRecord item = records[i];
                _list.Add(item);
                AddToDictionary(item, true);
            }
        }

        /// <summary>
        /// The GetLineByTag method returns a <see cref="GEDCOMRecord"/> by its tagName property.
        /// </summary>
        /// <param name="tagName">The provided tagName.</param>
        /// <returns>A <see cref="GEDCOMRecord"/> object (null if not present).</returns>
        public GEDCOMRecord GetLineByTag(GEDCOMTag tagName)
        {
            GEDCOMRecord item = null;
            bool bFound = TryGetRecord(tagName, out item);
            return item;
        }

        /// <summary>
        /// The GetLineByTag method returns a <see cref="GEDCOMRecord"/> by its tagName property.
        /// </summary>
        /// <typeparam name="TRecord">The type of the record.</typeparam>
        /// <param name="tagName">The provided tagName.</param>
        /// <returns>A <see cref="GEDCOMRecord"/> object (null if not present).</returns>
        public TRecord GetLineByTag<TRecord>(GEDCOMTag tagName) where TRecord : GEDCOMRecord
        {
            return GetLineByTag(tagName) as TRecord;
        }

        /// <summary>
        /// The GetLinesByTag method returns a GEDCOMRecords Collection of lines with
        /// the same tagName property.
        /// </summary>
        /// <param name="tagName">The provided tagName.</param>
        /// <returns>A GEDCOMRecords collection (empty if not present).</returns>
        public GEDCOMRecordList GetLinesByTag(GEDCOMTag tagName)
        {
            List<GEDCOMRecord> tagList = null;
            GEDCOMRecordList records = new GEDCOMRecordList();
            bool bFound = _tagDictionary.TryGetValue(tagName, out tagList);

            if (bFound)
            {
                foreach (GEDCOMRecord record in tagList)
                {
                    records.Add(record);
                }
            }

            return records;
        }

        /// <summary>
        /// The GetLinesByTag method returns a List<TRecord> of lines with
        /// the same tagName property.
        /// </summary>
        /// <typeparam name="TRecord"></typeparam>
        /// <param name="tagName">The provided tagName.</param>
        /// <returns>A List<TRecord> (empty if not present).</returns>
        public List<TRecord> GetLinesByTag<TRecord>(GEDCOMTag tagName) where TRecord : GEDCOMRecord
        {
            List<GEDCOMRecord> tagList = null;
            List<TRecord> records = new List<TRecord>();
            bool bFound = _tagDictionary.TryGetValue(tagName, out tagList);

            if (bFound)
            {
                foreach (GEDCOMRecord record in tagList)
                {
                    records.Add(record as TRecord);
                }
            }

            return records;
        }

        /// <summary>
        /// The GetLinesByTags method returns a GEDCOMRecords Collection of lines with
        /// one of the tags in the passed in string.
        /// </summary>
        /// <typeparam name="TRecord"></typeparam>
        /// <param name="tags">The provided list of tags.</param>
        /// <returns>A GEDCOMRecords collection (empty if not present).</returns>
        public List<TRecord> GetLinesByTags<TRecord>(string tags) where TRecord : GEDCOMRecord
        {
            List<TRecord> retValue = new List<TRecord>();

            for (int i = 0; i < Count; i++)
            {
                if (tags.Trim().Contains(this[i].Tag.Trim()))
                {
                    retValue.Add(this[i] as TRecord);
                }
            }
            return retValue;
        }

        public int GetNextId(GEDCOMTag tagName)
        {
            return (_maxIdDictionary.ContainsKey(tagName)) ? _maxIdDictionary[tagName] + 1 : 1;
        }

        /// <summary>
        /// GetRecordData fetches the Data for the specified Record.
        /// </summary>
        /// <param name="tagName">The tag to fetch the data for.</param>
        /// <returns>A string.</returns>
        public string GetRecordData(GEDCOMTag tagName)
        {
            string tagData = string.Empty;
            GEDCOMRecord record = GetLineByTag(tagName);

            if (record != null && tagData != null)
            {
                tagData = record.Data;
            }

            return tagData;
        }

        /// <summary>
        /// GetXRefID fetches the XREfID for the specified Record.
        /// </summary>
        /// <param name="tagName">The tag to fetch the data for.</param>
        /// <returns>A string.</returns>
        public string GetXRefID(GEDCOMTag tagName)
        {
            string xRefID = string.Empty;
            GEDCOMRecord record = GetLineByTag(tagName);

            if (record != null && record.XRefId != null)
            {
                xRefID = record.XRefId;
            }
            return xRefID;
        }

        /// <summary>
        /// The GetXRefIDs method returns a string Collection of xRefIDs with
        /// the tagName specified.
        /// </summary>
        /// <param name="tagName">The tag.</param>
        /// <returns>A string collection (empty if not present).</returns>
        public List<string> GetXRefIDs(GEDCOMTag tagName)
        {
            List<string> xRefIDs = null;
            GEDCOMRecordList records = GetLinesByTag(tagName);

            if (records != null)
            {
                xRefIDs = new List<string>();

                for (int i = 0; i < records.Count; i++)
                {
                    xRefIDs.Add(records[i].XRefId);
                }
            }
            return xRefIDs;
        }

        /// <summary>
        /// This IndexOf method returns the index of the GEDCOMRecord with the
        /// provided Id.
        /// </summary>
        /// <param name="Id">The xRefId of the GEDCOMRecord.</param>
        /// <returns>The index.</returns>
        public int IndexOf(string id)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// ToList returns a List representing the GEDCOMRecordList.
        /// </summary>
        /// <returns>A List<GEDCOMRecord></returns>
        public List<GEDCOMRecord> ToList()
        {
            List<GEDCOMRecord> records = new List<GEDCOMRecord>();

            for (int i = 0; i < Count; i++)
            {
                records.Add(this[i]);
            }
            return records;
        }

        /// <summary>
        /// ToString creates a string representation of the GEDCOMRecordList.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (GEDCOMRecord record in _list)
            {
                sb.AppendLine(record.ToString());
            }

            return sb.ToString();
        }

        #endregion Public Methods

        #region IList<GEDCOMRecord> Members

        public int IndexOf(GEDCOMRecord item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, GEDCOMRecord item)
        {
            _list.Insert(index, item);
            AddToDictionary(item, true);
        }

        public void RemoveAt(int index)
        {
            RemoveFromDictionary(_list[index]);
            _list.RemoveAt(index);
        }

        public GEDCOMRecord this[int index]
        {
            get => _list[index];
            set
            {
                _list[index] = value;
                AddToDictionary(value, false);
            }
        }

        public void Add(GEDCOMRecord item)
        {
            _list.Add(item);
            AddToDictionary(item, true);
        }

        public void Clear()
        {
            _list.Clear();
            _tagDictionary.Clear();
        }

        public bool Contains(GEDCOMRecord item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(GEDCOMRecord[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
            foreach (GEDCOMRecord item in array)
            {
                AddToDictionary(item, true);
            }
        }

        public int Count
        {
            get => _list.Count;
        }

        public bool IsReadOnly
        {
            get => false;
        }

        public bool Remove(GEDCOMRecord item)
        {
            RemoveFromDictionary(item);
            return _list.Remove(item);
        }

        public IEnumerator<GEDCOMRecord> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion IList<GEDCOMRecord> Members
    }
}