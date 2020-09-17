using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiREV
{
    public class Present_NotPresent : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Not present","2 - Present" });
        }
    }
    public class Low_Medium_High : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Low","2 - Medium","3 - High" });
        }
    }
    public class Road_Works : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - No road works","2 - Minor road works in progress","3 - Major road works in progress" });
        }
    }
    public class Star_Rating : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - 1 Star", "2 - 2 Star", "3 - 3 Star", "4 - 4 Star", "5 - 5 Star", "6 - Not applicable" });
        }
    }
    public class Rural_Urban : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Rural / open area", "2 - Urban / rural town or village" });
        }
    }
    public class Land_Use : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Undeveloped areas", "2 - Farming and agricultural", "3 - Residential", "4 - Commercial", "5 - Not Recorded", "6 - Educational", "7 - Industrial and manufacturing" });
        }
    }
    public class Speed_Limits : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - <30km/h", "2 - 35km/h", "3 - 40km/h", "4-  45km/h", "5 - 50km/h", "6 - 55km/h", "7 - 60km/h", "8 - 65km/h", "9 - 70km/h", "10 - 75km/h", "11 - 80km/h", "12 - 85km/h", "13 - 90km/h", "14 - 95km/h", "15 - 100km/h", "16 - 105km/h", "17 - 110km/h", "18 - 115km/h", "19 - 120km/h", "20 - 125km/h", "21 - 130km/h", "22 - 135km/h", "23 - 140km/h", "24 - 145km/h", "25 - >=150km/h", "31 - <24mph", "32 - 25mph", "33 - 30mph", "34 - 35mph", "35 - 40mph", "36 - 45mph", "37 - 50mph", "38 - 55mph", "39 - 60mph", "40 - 65mph", "41 - 70mph", "42 - 75mph", "43 - 80mph", "44 - 85mph", "45 - >=90mph" });
        }
    }
    public class School_Zone : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - School zone flashing beacons", "2 - School zone static signs or road markings", "3 - No school zone warning", "4 - Not applicable (no school at the location)" });
        }
    }
    public class Motorcycle_Percent : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Not recorded", "2 - 0%", "3 - 1% to 5%", "4 - 6% to 10%", "5 - 11% to 20%", "6 - 21% to 40%", "7 - 41% to 60%", "8 - 61% to 80%", "9 - 81% to 99%", "10 - 100%" });
        }
    }
    public class Motorcycle_Observed : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - None", "2 - 1 motorcycle observed", "3 - 2 to 3 motorcycles observed", "4 - 4 to 5 motorcycles observed", "5 - 6 to 7 motorcycles observed", "6 - 8+ motorcycles observed" });
        }
    }
    public class Bicycle_Observed : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - None", "2 - 1 bicycle observed", "3 - 2 to 3 bicycles observed", "4 - 4 to 5 bicycles observed", "5 - 6 to 7 bicycles observed", "6 - 8+ bicycles observed" });
        }
    }
    public class Pedestrian_Observed : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - None", "2 - 1 pedestrian crossing observed", "3 - 2 to 3 pedestrians crossing observed", "4 - 4 to 5 pedestrians crossing observed", "5 - 6 to 7 pedestrians crossing observed", "6 - 8+ pedestrians crossing observed" });
        }
    }
    public class Peak_Hour_Flow : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - None", "2 - 1 to 5", "3 - 6 to 25", "4 - 26 to 50", "5 - 51 to 100", "6 - 101 to 200", "7 - 201 to 300", "8 - 301 to 400", "9 - 401 to 500", "10 - 501 to 900", "11 - 900+" });
        }
    }
    public class Curve_Type : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Straight or gently curving", "2 - Moderate", "3 - Sharp", "4 - Very sharp" });
        }
    }
    public class Curve_Grade : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - >= 0% to <7.5%", "2 - Not applicable", "3 - Not applicable", "4 - >= 7.5% to <10%", "5 - >= 10%" });
        }
    }
    public class Adequate_Poor_NotApplicable : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Adequate", "2 - Poor", "3 - Not applicable" });
        }
    }
    public class Adequate_Poor : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Adequate", "2 - Poor" });
        }
    }
    public class Meet_NotMeet_Specification : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Meets specification", "2 - Does not meet specification" });
        }
    }
    public class Carriageway : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Carriageway A of a divided carriageway road", "2 - Carriageway B of a divided carriageway road", "3 - Undivided road", "4 - Carriageway A of a motorcycle facility", "5 - Carriageway B of a motorcycle facility" });
        }
    }
    public class Median_Type : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Safety barrier - metal", "2 - Safety barrier - concrete", "3 - Physical median width >= 20.0m", "4 - Physical median width >= 10.0m to < 20.0m", "5 - Physical median width >= 5.0m to < 10.0m", "6 - Physical median width >= 1.0m to < 5.0m", "7 - Physical median width >= 0m to < 1.0m", "8 - Continuous central turning lane", "9 - Flexipost", "10 - Central hatching (>1m)", "11 - Centre line", "12 - Safety barrier - motorcycle friendly", "13 - One way", "14 - Wide centre line (0.3m to 1m)", "15 - Safety barrier - wire rope" });
        }
    }
    public class Lane_Number : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - One", "2 - Two", "3 - Three", "4 - Four or more", "5 - Two and one", "6 - Three and two" });
        }
    }
    public class Lane_Width : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Wide (>= 3.25m)", "2 - Medium (>= 2.75m to < 3.25m)", "3 - Narrow (>= 0m to < 2.75m)" });
        }
    }
    public class Pave_shoulder : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Wide (>= 2.4m)", "2 - Medium (>= 1.0m to < 2.4m)", "3 - Narrow (>= 0m to < 1.0m)", "4 - None" });
        }
    }
    public class Intersection_Type : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Merge lane", "2 - Roundabout", "3 - 3-leg (unsignalised) with protected turn lane", "4 - 3-leg (unsignalised) with no protected turn lane", "5 - 3-leg (signalised) with protected turn lane", "6 - 3-leg (signalised) with no protected turn lane", "7 - 4-leg (unsignalised) with protected turn lane", "8 - 4-leg (unsignalised) with no protected turn lane", "9 - 4-leg (signalised) with protected turn lane", "10 - 4-leg (signalised) with no protected turn lane", "11 - Do not use this code", "12 - None", "13 - Railway Crossing - passive (signs only)", "14 - Railway Crossing - active (flashing lights / boom gates)", "15 - Median crossing point - informal", "16 - Median crossing point - formal", "17 - Mini roundabout" });
        }
    }
    public class Intersection_Volume : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - >=15,000 vehicles", "2 - 10,000 to 15,000 vehicles", "3 - 5,000 to 10,000 vehicles", "4 - 1,000 to 5,000 vehicles", "5 - 100 to 1,000 vehicles", "6 - 1 to 100 vehicles", "7 - None" });
        }
    }
    public class Property_Access : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Commercial Access 1+", "2 - Residential Access 3+", "3 - Residential Access 1 or 2", "4 - None" });
        }
    }
    public class Roadside_Severity : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Safety barrier - metal", "2 - Safety barrier - concrete", "3 - Safety barrier - motorcycle friendly", "4 - Safety barrier - wire rope", "5 - Aggressive vertical face", "6 - Upwards slope - rollover gradient", "7 - Upwards slope - no rollover gradient", "8 - Deep drainage ditch", "9 - Downwards slope", "10 - Cliff", "11 - Tree >=10cm dia.", "12 - Sign, post or pole >= 10cm dia.", "13 - Non-frangible structure/bridge or building", "14 - Frangible structure or building", "15 - Unprotected safety barrier end", "16 - Large boulders >=20cm high", "17 - None" });
        }
    }
    public class Roadside_Severity_Distance : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - 0 to <1m", "2 - 1 to <5m", "3 - 5 to <10m", "4 - >=10m" });
        }
    }
    public class Good_Medium_Poor : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Good", "2 - Medium", "3 - Poor" });
        }
    }
    public class Skid_Resistance : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Sealed (adequate)", "2 - Sealed (medium)", "3 - Sealed (poor)", "4 - Unsealed (adequate)", "5 - Unsealed (poor)" });
        }
    }
    public class Pedestrian_Facilities : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Grade separated facility", "2 - Signalised with refuge", "3 - Signalised without refuge", "4 - Unsignalised marked crossing with refuge", "5 - Unsignalised marked crossing without a refuge", "6 - Refuge only", "7 - No facility", "14 - Unsignalised raised marked crossing with refuge", "15 - Unsignalised raised marked crossing without refuge", "16 - Raised unmarked crossing with refuge", "17 - Raised unmarked crossing without refuge" });
        }
    }
    public class School_Zone_Supervisor : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - School zone crossing supervisor present at school start and finish times", "2 - School zone crossing supervisor not present", "3 - Not applicable (no school at the location)" });
        }
    }
    public class Sidewalk : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Physical barrier", "2 - Non-physical separation >= 3.0m", "3 - Non-physical separation 1.0m to <3.0m", "4 - Non-physical separation 0m to <1.0m", "5  - None", "6 - Informal path >= 1.0m", "7 - Informal path 0m to <1.0m" });
        }
    }
    public class Bicycle_Facilities : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Off-road path with barrier", "2 - Off-road path", "3 - On-road lane", "4 - None", "5 - Extra wide outside (>=4.2m)", "6 - Signed shared roadway", "7 - Shared use path" });
        }
    }
    public class Motorised_Facilities : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new String[] { "1 - Exclusive one way motorcycle path with barrier", "2  -Exclusive one way motorcycle path without barrier", "3 - Exclusive two way motorcycle path with barrier", "4 - Exclusive two way motorcycle path without barrier", "5 - Inclusive motorcycle lane on roadway", "6 - None" });
        }
    }

    public class CodeSettings1
    {

        // Category #1
        private const String Category01 = "Category #01:   Frame Particulars";
    
        private string coder_name = "";
        private DateTime coding_date = DateTime.Now.Date;
        private DateTime road_survey_date = new DateTime();
        private string image_reference = "";
        private string road_name = "";
        private string section = "";
        private string distance = "";
        private string length = "";
        private string latitude = "";
        private string longitude = "";
        private string landmark = "";
        private string comments = "";
        private string upgrade_cost = "";
        private string roadworks = "";

        [Category(Category01), DisplayName("[1.01]  Coder Name"), Description("[Coder_name]")]
        public string _1_01_Coder_name
        {
            get { return coder_name; }
            set { coder_name = value; }
        }
    
        [Category(Category01), DisplayName("[1.02]  Coding Date"), Description("[Coding_date]")]
        public DateTime _1_02_Coding_date
        {
            get { return coding_date; }
            set { coding_date = value; }
        }

        [Category(Category01), DisplayName("[1.03]  Road Survey Date"), Description("[Road_survey_date]")]
        public DateTime _1_03_Road_survey_date
        {
            get { return road_survey_date; }
            set { road_survey_date = value; }
        }

        [Category(Category01), DisplayName("[1.04]  Image Reference"), Description("[Image_reference]")]
        public String _1_04_Image_reference
        {
            get { return image_reference; }
            set { image_reference = value; }
        }

        [Category(Category01), DisplayName("[1.05]  Road Name"), Description("[Road_name]")]
        public String _1_05_Road_name
        {
            get { return road_name; }
            set { road_name = value; }
        }

        [Category(Category01), DisplayName("[1.06]  Section"), Description("[Section]")]
        public String _1_06_Section
        {
            get { return section; }
            set { section = value; }
        }

        [Category(Category01), DisplayName("[1.07]  Distance"), Description("[Distance]")]
        public String _1_07_Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        [Category(Category01), DisplayName("[1.08]  Length"), Description("[Length]")]
        public String _1_08_Length
        {
            get { return length; }
            set { length = value; }
        }

        [Category(Category01), DisplayName("[1.09]  Latitude"), Description("[Latitude]")]
        public String _1_09_Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        [Category(Category01), DisplayName("[1.10]  Longitude"), Description("[Longitude]")]
        public String _1_10_Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        [Category(Category01), DisplayName("[1.11]  Landmark"), Description("[Landmark]")]
        public String _1_11_Landmark
        {
            get { return landmark; }
            set { landmark = value; }
        }

        [Category(Category01), DisplayName("[1.12]  Comments"), Description("[Comments]")]
        public String _1_12_Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        [TypeConverter(typeof(Low_Medium_High)), Category(Category01), DisplayName("[1.13]  Upgrade Cost"), Description("[Upgrade_cost]")]
        public String _1_13_Upgrade_cost
        {
            get { return upgrade_cost; }
            set { upgrade_cost = value; }
        }

        [TypeConverter(typeof(Road_Works)), Category(Category01), DisplayName("[1.14]  Roadworks"), Description("[Roadworks]")]
        public String _1_14_Roadworks
        {
            get { return roadworks; }
            set { roadworks = value; }
        }
        // Category #1 End


        // Category #2
        private string Annual_Fatality_Growth_Multiplier = "";
        private string Vehicle_occupant_star_rating_policy_target = "";
        private string Motorcycle_star_rating_policy_target = "";
        private string Pedestrian_star_rating_policy_target = "";
        private string Bicycle_star_rating_policy_target = "";
        //private string Road_survey_date = "";

        private const String Category02 = "Category #02:   ";

        [Category(Category02), DisplayName("[2.01]  Annual fatality Growth Multipier"), Description("[Annual_fatality_growth_multiplier]")]
        public String _2_01_Annual_Fatality_Growth_Multiplier
        {
            get { return Annual_Fatality_Growth_Multiplier; }
            set { Annual_Fatality_Growth_Multiplier = value; }
        }

        [TypeConverter(typeof(Star_Rating)), Category(Category02), DisplayName("[2.02]  Vehicle Occupant Star Rating Policy Target"), Description("[Vehicle_occupant_star_rating_policy_target]")]
        public String _2_02_Vehicle_occupant_star_rating_policy_target
        {
            get { return Vehicle_occupant_star_rating_policy_target; }
            set { Vehicle_occupant_star_rating_policy_target = value; }
        }

        [TypeConverter(typeof(Star_Rating)), Category(Category02), DisplayName("[2.03]  Motorcycle Star Rating Policy Target"), Description("[Motorcycle_star_rating_policy_target]")]
        public String _2_03_Motorcycle_star_rating_policy_target
        {
            get { return Motorcycle_star_rating_policy_target; }
            set { Motorcycle_star_rating_policy_target = value; }
        }

        [TypeConverter(typeof(Star_Rating)), Category(Category02), DisplayName("[2.04]  Pedestrian Star Rating Policy Target"), Description("[Pedestrian_star_rating_policy_target]")]
        public String _2_04_Pedestrian_star_rating_policy_target
        {
            get { return Pedestrian_star_rating_policy_target; }
            set { Pedestrian_star_rating_policy_target = value; }
        }

        [TypeConverter(typeof(Star_Rating)), Category(Category02), DisplayName("[2.05]  Bicycle Star Rating Policy Target"), Description("[Bicycle_star_rating_policy_target]")]
        public String _2_05_Bicycle_star_rating_policy_target
        {
            get { return Bicycle_star_rating_policy_target; }
            set { Bicycle_star_rating_policy_target = value; }
        }

        //[Category(Category02), DisplayName("[2.06]  Road Survey Date"), Description("[Road_survey_date]")]
        //public String _2_06_Road_survey_date
        //{
        //    get { return Road_survey_date; }
        //    set { Road_survey_date = value; }
        //}

        // Category #2 End



        // Category #3
        private string Area_type = "";
        private string Land_use_driver_side = "";
        private string Land_use_passenger_side = "";
        private const String Category03 = "Category #03:   Area Type";

        [TypeConverter(typeof(Rural_Urban)), Category(Category03), DisplayName("[3.01]  Area Type"), Description("[Area_type]")]
        public String _3_01_Area_type
        {
            get { return Area_type; }
            set { Area_type = value; }
        }

        [TypeConverter(typeof(Land_Use)), Category(Category03), DisplayName("[3.02]  Land Use Driver Side"), Description("[Land_use_driver_side]")]
        public String _3_02_Land_use_driver_side
        {
            get { return Land_use_driver_side; }
            set { Land_use_driver_side = value; }
        }

        [TypeConverter(typeof(Land_Use)), Category(Category03), DisplayName("[3.03]  Land Use Passenger Side"), Description("[Land_use_passenger_side]")]
        public String _3_03_Land_use_passenger_side
        {
            get { return Land_use_passenger_side; }
            set { Land_use_passenger_side = value; }
        }
        // Category #3 End


        // Category #4
        private string Speed_limit = "";
        private string Motorcycle_speed_limit = "";
        private string Truck_speed_limit = "";
        private string Differential_speed_limits = "";
        private string Speed_management_traffic_calming = "";
        private string School_zone_warning = "";
        private const String Category04 = "Category #04:   Traffic Management";

        [TypeConverter(typeof(Speed_Limits)), Category(Category04), DisplayName("[4.01]  Speed Limit"), Description("[Speed_limit]")]
        public String _4_01_Speed_limit
        {
            get { return Speed_limit; }
            set { Speed_limit = value; }
        }

        [TypeConverter(typeof(Speed_Limits)), Category(Category04), DisplayName("[4.02]  Mototrcycle Speed Limit"), Description("[Motorcycle_speed_limit]")]
        public String _4_02_Motorcycle_speed_limit
        {
            get { return Motorcycle_speed_limit; }
            set { Motorcycle_speed_limit = value; }
        }

        [TypeConverter(typeof(Speed_Limits)), Category(Category04), DisplayName("[4.03]  Truck Speed Limit"), Description("[Truck_speed_limit]")]
        public String _4_03_Truck_speed_limit
        {
            get { return Truck_speed_limit; }
            set { Truck_speed_limit = value; }
        }

        [TypeConverter(typeof(Present_NotPresent)), Category(Category04), DisplayName("[4.04]  Differential Speeds"), Description("[Differential_speed_limits]")]
        public String _4_04_Differential_speed_limits
        {
            get { return Differential_speed_limits; }
            set { Differential_speed_limits = value; }
        }

        [TypeConverter(typeof(Present_NotPresent)), Category(Category04), DisplayName("[4.05]  Speed Management/Traffic calming"), Description("[Speed_management_traffic_calming]")]
        public String _4_05_Speed_management_traffic_calming
        {
            get { return Speed_management_traffic_calming; }
            set { Speed_management_traffic_calming = value; }
        }

        [TypeConverter(typeof(School_Zone)), Category(Category04), DisplayName("[4.06]  School Zone Warning"), Description("[School_zone_warning]")]
        public String _4_06_School_zone_warning
        {
            get { return School_zone_warning; }
            set { School_zone_warning = value; }
        }
        // Category #4 End
    }

    public class CodeSettings2
    {
        // Category #5
        private string Vehicle_flow_AADT = "";
        private string Motorcycle_PERCENT = "";
        private string Motorcycle_observed_flow = "";
        private string Bicycle_observed_flow = "";
        private string Bicycle_peak_hour_flow = "";
        private string Pedestrian_observed_flow_across_the_road = "";
        private string Pedestrian_observed_flow_along_the_road_driver_side = "";
        private string Pedestrian_observed_flow_along_the_road_passenger_side = "";
        private string Pedestrian_peak_hour_flow_across_the_road = "";
        private string Pedestrian_peak_hour_flow_along_the_road_driver_side = "";
        private string Pedestrian_peak_hour_flow_along_the_road_passenger_side = "";
        private string Operating_speed_85th_percentile = "";
        private string Operating_speed_mean = "";
        private const String Category05 = "Category #05:   Traffic Characteristics";

        [Category(Category05), DisplayName("[5.01]  Vehicle Flow (AADT)"), Description("[Vehicle_flow_AADT]")]
        public String _5_01_Vehicle_flow_AADT
        {
            get { return Vehicle_flow_AADT; }
            set { Vehicle_flow_AADT = value; }
        }

        [TypeConverter(typeof(Motorcycle_Percent)), Category(Category05), DisplayName("[5.02]  Motorcycle %"), Description("[Motorcycle_%]")]
        public String _5_02_Motorcycle_PERCENT
        {
            get { return Motorcycle_PERCENT; }
            set { Motorcycle_PERCENT = value; }
        }

        [TypeConverter(typeof(Motorcycle_Observed)), Category(Category05), DisplayName("[5.03]  Motorcycle Observed Flow"), Description("[Motorcycle_observed_flow]")]
        public String _5_03_Motorcycle_observed_flow
        {
            get { return Motorcycle_observed_flow; }
            set { Motorcycle_observed_flow = value; }
        }

        [TypeConverter(typeof(Bicycle_Observed)), Category(Category05), DisplayName("[5.04]  Bicycle Observed Flow"), Description("[Bicycle_observed_flow]")]
        public String _5_04_Bicycle_observed_flow
        {
            get { return Bicycle_observed_flow; }
            set { Bicycle_observed_flow = value; }
        }

        [TypeConverter(typeof(Peak_Hour_Flow)), Category(Category05), DisplayName("[5.05]  Bicycle Peak Hour Flow"), Description("[Bicycle_peak_hour_flow]")]
        public String _5_05_Bicycle_peak_hour_flow
        {
            get { return Bicycle_peak_hour_flow; }
            set { Bicycle_peak_hour_flow = value; }
        }

        [TypeConverter(typeof(Pedestrian_Observed)), Category(Category05), DisplayName("[5.06]  Pedestrian Observed Flow Across The Road"), Description("[Pedestrian_observed_flow_across_the_road]")]
        public String _5_06_Pedestrian_observed_flow_across_the_road
        {
            get { return Pedestrian_observed_flow_across_the_road; }
            set { Pedestrian_observed_flow_across_the_road = value; }
        }

        [TypeConverter(typeof(Pedestrian_Observed)), Category(Category05), DisplayName("[5.07]  Pedestrian Observed Flow Along The Road - Driver Side"), Description("[Pedestrian_observed_flow_along_the_road_driver_side]")]
        public String _5_07_Pedestrian_observed_flow_along_the_road_driver_side
        {
            get { return Pedestrian_observed_flow_along_the_road_driver_side; }
            set { Pedestrian_observed_flow_along_the_road_driver_side = value; }
        }

        [TypeConverter(typeof(Pedestrian_Observed)), Category(Category05), DisplayName("[5.08]  Pedestrian Observed Flow Along The Road - Passenger Side"), Description("[Pedestrian_observed_flow_along_the_road_passenger_side]")]
        public String _5_08_Pedestrian_observed_flow_along_the_road_passenger_side
        {
            get { return Pedestrian_observed_flow_along_the_road_passenger_side; }
            set { Pedestrian_observed_flow_along_the_road_passenger_side = value; }
        }

        [TypeConverter(typeof(Peak_Hour_Flow)), Category(Category05), DisplayName("[5.09]  Pedestrian Peak Hour Flow Across The Road"), Description("[Pedestrian_peak_hour_flow_across_the_road]")]
        public String _5_09_Pedestrian_peak_hour_flow_across_the_road
        {
            get { return Pedestrian_peak_hour_flow_across_the_road; }
            set { Pedestrian_peak_hour_flow_across_the_road = value; }
        }

        [TypeConverter(typeof(Peak_Hour_Flow)), Category(Category05), DisplayName("[5.10]  Pedestrian Peak Hour Flow Along The Road- Driver Side"), Description("[Pedestrian_peak_hour_flow_along_the_road_driver_side]")]
        public String _5_10_Pedestrian_peak_hour_flow_along_the_road_driver_side
        {
            get { return Pedestrian_peak_hour_flow_along_the_road_driver_side; }
            set { Pedestrian_peak_hour_flow_along_the_road_driver_side = value; }
        }

        [TypeConverter(typeof(Peak_Hour_Flow)), Category(Category05), DisplayName("[5.11]  Pedestrian Peak Hour Flow Along The Road- Passenger Side"), Description("[Pedestrian_peak_hour_flow_along_the_road_passenger_side]")]
        public String _5_11_Pedestrian_peak_hour_flow_along_the_road_passenger_side
        {
            get { return Pedestrian_peak_hour_flow_along_the_road_passenger_side; }
            set { Pedestrian_peak_hour_flow_along_the_road_passenger_side = value; }
        }

        [TypeConverter(typeof(Speed_Limits)), Category(Category05), DisplayName("[5.12]  Operating Speed (85th percentile)"), Description("[Operating_speed_85th_percentile]")]
        public String _5_12_Operating_speed_85th_percentile
        {
            get { return Operating_speed_85th_percentile; }
            set { Operating_speed_85th_percentile = value; }
        }

        [TypeConverter(typeof(Speed_Limits)), Category(Category05), DisplayName("[5.13]  Operating Speed (Mean)"), Description("[Operating_speed_mean]")]
        public String _5_13_Operating_speed_mean
        {
            get { return Operating_speed_mean; }
            set { Operating_speed_mean = value; }
        }
        // Category #5 End


        // Category #6
        private string Curvature = "";
        private string Quality_of_curve = "";
        private string Grade = "";
        private string Sight_distance = "";
        private string Roads_that_cars_can_read = "";
        private const String Category06 = "Category #06:   Alignment";

        [TypeConverter(typeof(Curve_Type)), Category(Category06), DisplayName("[6.01]  Curvature"), Description("[Curvature]")]
        public String _6_01_Curvature
        {
            get { return Curvature; }
            set { Curvature = value; }
        }

        [TypeConverter(typeof(Adequate_Poor_NotApplicable)), Category(Category06), DisplayName("[6.02]  Quality Of Curve"), Description("[Quality_of_curve]")]
        public String _6_02_Quality_of_curve
        {
            get { return Quality_of_curve; }
            set { Quality_of_curve = value; }
        }

        [TypeConverter(typeof(Curve_Grade)), Category(Category06), DisplayName("[6.03]  Grade"), Description("[Grade]")]
        public String _6_03_Grade
        {
            get { return Grade; }
            set { Grade = value; }
        }

        [TypeConverter(typeof(Adequate_Poor)), Category(Category06), DisplayName("[6.04]  Sight Distance"), Description("[Sight_distance]")]
        public String _6_04_Sight_distance
        {
            get { return Sight_distance; }
            set { Sight_distance = value; }
        }

        [TypeConverter(typeof(Meet_NotMeet_Specification)), Category(Category06), DisplayName("[6.05]  Roads That Cars Can Read"), Description("[Roads_that_cars_can_read]")]
        public String _6_05_Roads_that_cars_can_read
        {
            get { return Roads_that_cars_can_read; }
            set { Roads_that_cars_can_read = value; }
        }
        // Category #6 End


        // Category #7
        private string Carriageway_label = "";
        private string Median_type = "";
        private string Number_of_lanes = "";
        private string Lane_width = "";
        private string Paved_shoulder_driver_side = "";
        private string Paved_shoulder_passenger_side = "";

        private const String Category07 = "Category #07:   Cross Section";

        //[Browsable(true)]       

        [TypeConverter(typeof(Carriageway)), Category(Category07), DisplayName("[7.01]  Carriageway Label"), Description("[Carriageway_label]")]
        public String _7_01_Carriageway_label
        {
            get { return Carriageway_label; }
            set { Carriageway_label = value; }
        }

        [TypeConverter(typeof(Median_Type)), Category(Category07), DisplayName("[7.02]  Median Type"), Description("[Median_type]")]
        public String _7_02_Median_type
        {
            get { return Median_type; }
            set { Median_type = value; }
        }

        [TypeConverter(typeof(Lane_Number)), Category(Category07), DisplayName("[7.03]  Number Of Lanes"), Description("[Number_of_lanes]")]
        public String _7_03_Number_of_lanes
        {
            get { return Number_of_lanes; }
            set { Number_of_lanes = value; }
        }

        [TypeConverter(typeof(Lane_Width)), Category(Category07), DisplayName("[7.04]  Lane Width"), Description("[Lane_width]")]
        public String _7_04_Lane_width
        {
            get { return Lane_width; }
            set { Lane_width = value; }
        }

        [TypeConverter(typeof(Pave_shoulder)), Category(Category07), DisplayName("[7.05]  Paved Shoulder - Driver Side"), Description("[Paved_shoulder_driver_side]")]
        public String _7_05_Paved_shoulder_driver_side
        {
            get { return Paved_shoulder_driver_side; }
            set { Paved_shoulder_driver_side = value; }
        }

        [TypeConverter(typeof(Pave_shoulder)), Category(Category07), DisplayName("[7.06]  Paved Shoulder - Passenger Side"), Description("[Paved_shoulder_passenger_side]")]
        public String _7_06_Paved_shoulder_passenger_side
        {
            get { return Paved_shoulder_passenger_side; }
            set { Paved_shoulder_passenger_side = value; }
        }
        // Category #7 End


        // Category #8
        private string Intersection_type = "";
        private string Intersection_channelisation = "";
        private string Intersecting_road_volume = "";
        private string Intersection_quality = "";
        private string Property_access_points = "";
        private const String Category08 = "Category #08:   Intersection / Access Points";

        [TypeConverter(typeof(Intersection_Type)), Category(Category08), DisplayName("[8.01]  Intersection Type"), Description("[Intersection_type]")]
        public String _8_01_Intersection_type
        {
            get { return Intersection_type; }
            set { Intersection_type = value; }
        }

        [TypeConverter(typeof(Present_NotPresent)), Category(Category08), DisplayName("[8.02]  Intersection Channelization"), Description("[Intersection_channelisation]")]
        public String _8_02_Intersection_channelisation
        {
            get { return Intersection_channelisation; }
            set { Intersection_channelisation = value; }
        }

        [TypeConverter(typeof(Intersection_Volume)), Category(Category08), DisplayName("[8.03]  Intersecting Road Volume"), Description("[Intersecting_road_volume]")]
        public String _8_03_Intersecting_road_volume
        {
            get { return Intersecting_road_volume; }
            set { Intersecting_road_volume = value; }
        }

        [TypeConverter(typeof(Adequate_Poor_NotApplicable)), Category(Category08), DisplayName("[8.04]  Intersection Quality"), Description("[Intersection_quality]")]
        public String _8_04_Intersection_quality
        {
            get { return Intersection_quality; }
            set { Intersection_quality = value; }
        }

        [TypeConverter(typeof(Property_Access)), Category(Category08), DisplayName("[8.05]  Property Access Points"), Description("[Property_access_points]")]
        public String _8_05_Property_access_points
        {
            get { return Property_access_points; }
            set { Property_access_points = value; }
        }
        // Category #8 End
    }

    public class CodeSettings3
    {
        // Category #9
        private string Roadside_severity_driver_side_object = "";
        private string Roadside_severity_driver_side_distance = "";
        private string Roadside_severity_passenger_side_object = "";
        private string Roadside_severity_passenger_side_distance = "";
        private string Vehicle_parking = "";
        private string Service_road = "";
        private const String Category09 = "Category #09:   Roadside Features";


        [TypeConverter(typeof(Roadside_Severity)), Category(Category09), DisplayName("[9.01]  Roadside Severity - Driver Side Object"), Description("[Roadside_severity_driver_side_object]")]
        public String _9_01_Roadside_severity_driver_side_object
        {
            get { return Roadside_severity_driver_side_object; }
            set { Roadside_severity_driver_side_object = value; }
        }

        [TypeConverter(typeof(Roadside_Severity_Distance)), Category(Category09), DisplayName("[9.02]  Roadside Severity - Driver Side Distance"), Description("[Roadside_severity_driver_side_distance]")]
        public String _9_02_Roadside_severity_driver_side_distance
        {
            get { return Roadside_severity_driver_side_distance; }
            set { Roadside_severity_driver_side_distance = value; }
        }

        [TypeConverter(typeof(Roadside_Severity)), Category(Category09), DisplayName("[9.03]  Roadside Severity - Passenger Side Object"), Description("[Roadside_severity_passenger_side_object]")]
        public String _9_03_Roadside_severity_passenger_side_object
        {
            get { return Roadside_severity_passenger_side_object; }
            set { Roadside_severity_passenger_side_object = value; }
        }

        [TypeConverter(typeof(Roadside_Severity_Distance)), Category(Category09), DisplayName("[9.04]  Roadside Severity - Passenger Side Distance"), Description("[Roadside_severity_passenger_side_distance]")]
        public String _9_04_Roadside_severity_passenger_side_distance
        {
            get { return Roadside_severity_passenger_side_distance; }
            set { Roadside_severity_passenger_side_distance = value; }
        }

        [TypeConverter(typeof(Low_Medium_High)), Category(Category09), DisplayName("[9.05]  Vehicle Parking"), Description("[Vehicle_parking]")]
        public String _9_05_Vehicle_parking
        {
            get { return Vehicle_parking; }
            set { Vehicle_parking = value; }
        }

        [TypeConverter(typeof(Present_NotPresent)), Category(Category09), DisplayName("[9.06]  Service Road"), Description("[Service_road]")]
        public String _9_06_Service_road
        {
            get { return Service_road; }
            set { Service_road = value; }
        }
        // Category #9 End


        // Category #10
        private string Street_lighting = "";
        private string Delineation = "";
        private string Centreline_rumble_strips = "";
        private string Shoulder_rumble_strips = "";
        private const String Category10 = "Category #10:   Visual Aid";

        [TypeConverter(typeof(Present_NotPresent)), Category(Category10), DisplayName("[10.01]  Street Lighting"), Description("[Street_lighting]")]
        public String _10_01_Street_lighting
        {
            get { return Street_lighting; }
            set { Street_lighting = value; }
        }

        [TypeConverter(typeof(Adequate_Poor)), Category(Category10), DisplayName("[10.02]  Delineation"), Description("[Delineation]")]
        public String _10_02_Delineation
        {
            get { return Delineation; }
            set { Delineation = value; }
        }

        [TypeConverter(typeof(Present_NotPresent)), Category(Category10), DisplayName("[10.03]  Centreline Rumble Strips"), Description("[Centreline_rumble_strips]")]
        public String _10_03_Centreline_rumble_strips
        {
            get { return Centreline_rumble_strips; }
            set { Centreline_rumble_strips = value; }
        }

        [TypeConverter(typeof(Present_NotPresent)), Category(Category10), DisplayName("[10.04]  Shoulder Rumble Strips"), Description("[Shoulder_rumble_strips]")]
        public String _10_04_Shoulder_rumble_strips
        {
            get { return Shoulder_rumble_strips; }
            set { Shoulder_rumble_strips = value; }
        }
        // Category #10 End


        // Category #11
        private string Road_condition = "";
        private string Skid_resistance = "";
        private const String Category11 = "Category #11:   Pavement Surface";

        [TypeConverter(typeof(Good_Medium_Poor)), Category(Category11), DisplayName("[11.01]  Road Condition"), Description("[Road_condition]")]
        public String _11_01_Road_condition
        {
            get { return Road_condition; }
            set { Road_condition = value; }
        }

        [TypeConverter(typeof(Skid_Resistance)), Category(Category11), DisplayName("[11.02]  Skid Resistance/Grip"), Description("[Skid_resistance]")]
        public String _11_02_Skid_resistance
        {
            get { return Skid_resistance; }
            set { Skid_resistance = value; }
        }
        // Category #11 End


        // Category #12
        private string Pedestrian_crossing_facilities_inspected_road = "";
        private string Pedestrian_crossing_quality = "";
        private string School_zone_crossing_supervisor = "";
        private string Pedestrian_crossing_facilities_intersecting_road = "";
        private string Sidewalk_driver_side = "";
        private string Sidewalk_passenger_side = "";
        private string Pedestrian_fencing = "";
        private string Facilities_for_bicycles = "";
        private string Facilities_for_motorised_two_wheelers = "";
        private const String Category12 = "Category #12:   Facilities for VRU";

        [TypeConverter(typeof(Pedestrian_Facilities)), Category(Category12), DisplayName("[12.01]  Pedestrian Crossing Facilities - Inspected Road"), Description("[Pedestrian_crossing_facilities_inspected_road]")]
        public String _12_01_Pedestrian_crossing_facilities_inspected_road
        {
            get { return Pedestrian_crossing_facilities_inspected_road; }
            set { Pedestrian_crossing_facilities_inspected_road = value; }
        }

        [TypeConverter(typeof(Adequate_Poor_NotApplicable)), Category(Category12), DisplayName("[12.02]  Pedestrian Crossing Quality"), Description("[Pedestrian_crossing_quality]")]
        public String _12_02_Pedestrian_crossing_quality
        {
            get { return Pedestrian_crossing_quality; }
            set { Pedestrian_crossing_quality = value; }
        }

        [TypeConverter(typeof(School_Zone_Supervisor)), Category(Category12), DisplayName("[12.03]  School Zone Crossing Supervisor"), Description("[School_zone_crossing_supervisor]")]
        public String _12_03_School_zone_crossing_supervisor
        {
            get { return School_zone_crossing_supervisor; }
            set { School_zone_crossing_supervisor = value; }
        }

        [TypeConverter(typeof(Pedestrian_Facilities)), Category(Category12), DisplayName("[12.04]  Pedestrian Crossing Facilities - Intersecting Road"), Description("[Pedestrian_crossing_facilities_intersecting_road]")]
        public String _12_04_Pedestrian_crossing_facilities_intersecting_road
        {
            get { return Pedestrian_crossing_facilities_intersecting_road; }
            set { Pedestrian_crossing_facilities_intersecting_road = value; }
        }

        [TypeConverter(typeof(Sidewalk)), Category(Category12), DisplayName("[12.05]  Sidewalk - Driver Side"), Description("[Sidewalk_driver_side]")]
        public String _12_05_Sidewalk_driver_side
        {
            get { return Sidewalk_driver_side; }
            set { Sidewalk_driver_side = value; }
        }

        [TypeConverter(typeof(Sidewalk)), Category(Category12), DisplayName("[12.06]  Sidewalk - Passenger Side"), Description("[Sidewalk_passenger_side]")]
        public String _12_06_Sidewalk_passenger_side
        {
            get { return Sidewalk_passenger_side; }
            set { Sidewalk_passenger_side = value; }
        }

        [TypeConverter(typeof(Present_NotPresent)), Category(Category12), DisplayName("[12.07]  Pedestrian Fencing"), Description("[Pedestrian_fencing]")]
        public String _12_07_Pedestrian_fencing
        {
            get { return Pedestrian_fencing; }
            set { Pedestrian_fencing = value; }
        }

        [TypeConverter(typeof(Bicycle_Facilities)), Category(Category12), DisplayName("[12.08]  Facilities For Bicycles"), Description("[Facilities_for_bicycles]")]
        public String _12_08_Facilities_for_bicycles
        {
            get { return Facilities_for_bicycles; }
            set { Facilities_for_bicycles = value; }
        }

        [TypeConverter(typeof(Motorised_Facilities)), Category(Category12), DisplayName("[12.09]  Facilities For Motorized Two Wheelers"), Description("[Facilities_for_motorised_two_wheelers]")]
        public String _12_09_Facilities_for_motorised_two_wheelers
        {
            get { return Facilities_for_motorised_two_wheelers; }
            set { Facilities_for_motorised_two_wheelers = value; }
        }
        // Category #12 End

    }
}