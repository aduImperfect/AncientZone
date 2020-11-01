using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializableDictionaryExample : MonoBehaviour {
	// The dictionaries can be accessed throught a property
	[SerializeField]
	StringStringDictionary m_stringStringDictionary = null;
	public IDictionary<string, string> StringStringDictionary
	{
		get { return m_stringStringDictionary; }
		set { m_stringStringDictionary.CopyFrom (value); }
	}

	public ObjectColorDictionary m_objectColorDictionary;
	public StringColorArrayDictionary m_objectColorArrayDictionary;

	void Reset ()
	{
        // access by property
        StringStringDictionary = new Dictionary<string, string>();

        m_stringStringDictionary.Add("first key", "value A");
        m_stringStringDictionary.Add("second key", "value B");
        m_stringStringDictionary.Add("third key", "value C");
		m_objectColorDictionary = new ObjectColorDictionary() { {gameObject, Color.blue}, {this, Color.red} };
	}
}
