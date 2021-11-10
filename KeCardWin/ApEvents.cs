using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;


namespace KeCardWin
{
    public class ApEvents
    {
        public ApEvent[] apEvents;


        // 最初に条件にマッチするApEventを返す
        public ApEvent SearchFirst( ApEvent.EVENT_TYPE evType , int subType )
        {
            if (apEvents == null) return null;

            foreach( ApEvent ev in apEvents )
            {
                if( ev.eventType == evType && ev.subEventType == subType )
                {
                    return ev;
                }
            }
            return null;
        }

        // Voiceキーワードを列挙
        public string[] GetVoiceKeywords()
        {
            if (apEvents == null) return null;

            var lst = apEvents.Where(x => x.eventType == ApEvent.EVENT_TYPE.VOICE).Select( x => ((ApEventVoice)x).keyword);

            return lst.ToArray();
        }

        // 転送データあり？
        public bool TxDataExists()
        {
            if (apEvents == null) return false;

            foreach( ApEvent ev in apEvents )
            {
                if (ev.txDataExists) return true;
            }

            return false;
        }

        // BleConnectionが必要？
        public bool BleConnectionNeeds()
        {
            if (apEvents == null) return false;

            foreach (ApEvent ev in apEvents)
            {
                if (ev.bleConnectionNeeds) return true;
            }

            return false;
        }

    }

    public class ApEventsLib
    {
        public const string XML_FILE_NAME = @"ApEvents.xml";

        static XmlAttributes GetAttributes()
        {
            // Each overridden field, property, or type requires
            // an XmlAttributes instance.  
            XmlAttributes attrs = new XmlAttributes();

            // Creates an XmlElementAttribute instance to override the
            // field that returns Book objects. The overridden field  
            // returns Expanded objects instead.  
            XmlElementAttribute attrEventTimer = new XmlElementAttribute();
            attrEventTimer.ElementName = "ApEventTimer";
            attrEventTimer.Type = typeof(ApEventTimer);
            attrs.XmlElements.Add(attrEventTimer);

            XmlElementAttribute attrEventButton = new XmlElementAttribute();
            attrEventButton.ElementName = "ApEventButton";
            attrEventButton.Type = typeof(ApEventButton);
            attrs.XmlElements.Add(attrEventButton);

            XmlElementAttribute attrEventPc = new XmlElementAttribute();
            attrEventPc.ElementName = "ApEventPc";
            attrEventPc.Type = typeof(ApEventPc);
            attrs.XmlElements.Add(attrEventPc);

            XmlElementAttribute attrEventVoice = new XmlElementAttribute();
            attrEventVoice.ElementName = "ApEventVoice";
            attrEventVoice.Type = typeof(ApEventVoice);
            attrs.XmlElements.Add(attrEventVoice);

            XmlElementAttribute attrEventInit = new XmlElementAttribute();
            attrEventInit.ElementName = "ApEventInit";
            attrEventInit.Type = typeof(ApEventInit);
            attrs.XmlElements.Add(attrEventInit);


            return attrs;
        }

        static public void SerializeObject(ApEvents apEvents )
        {
            XmlAttributes attrs = GetAttributes();

            // Creates the XmlAttributeOverrides instance.  
            XmlAttributeOverrides attrOverrides = new XmlAttributeOverrides();

            // Adds the type of the class that contains the overridden
            // member, as well as the XmlAttributes instance to override it
            // with, to the XmlAttributeOverrides.  
            attrOverrides.Add(typeof(ApEvents), "apEvents", attrs);

            // Creates the XmlSerializer using the XmlAttributeOverrides.  
            XmlSerializer s =
            new XmlSerializer(typeof(ApEvents), attrOverrides);

            // Writing the file requires a TextWriter instance.  
            TextWriter writer = new StreamWriter(XML_FILE_NAME);

            // Serializes the object.  
            s.Serialize(writer, apEvents);
            writer.Close();
        }

        static public ApEvents DeserializeObject()
        {
            try
            {
                XmlAttributeOverrides attrOverrides =
                    new XmlAttributeOverrides();


                XmlAttributes attrs = GetAttributes();

                attrOverrides.Add(typeof(ApEvents), "apEvents", attrs);

                // Creates the XmlSerializer using the XmlAttributeOverrides.  
                XmlSerializer s =
                new XmlSerializer(typeof(ApEvents), attrOverrides);

                FileStream fs = new FileStream(XML_FILE_NAME, FileMode.Open);
                ApEvents apEvents = (ApEvents)s.Deserialize(fs);
                // Console.WriteLine("ExpandedBook:");

                fs.Close();

                // The difference between deserializing the overridden
                // XML document and serializing it is this: To read the derived
                // object values, you must declare an object of the derived type
                // and cast the returned object to it.  

                if(apEvents != null) return apEvents;

            }
            catch
            {

            }
            return new ApEvents();
        }

    }
}
