using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace MiREV
{
    class TableDumper
    {
        private int totalColumnToSkip = 1;
        private Boolean[] trimValue = new Boolean[] {
            false,  // Coder_name = 1,
            false,  // Coding_date = 2,
            false,  // Road_survey_date = 3,
            false,  // Image_reference = 4,
            false,  // Road_name = 5,
            false,  // Section = 6,
            false,  // Distance = 7,
            false,  // Length = 8,
            false,  // Latitude = 9,
            false,  // Longitude = 10,
            false,  // Landmark = 11,
            false,  // Comments = 12,
            true,   // Carriageway_label = 13,
            true,   // Upgrade_cost = 14,
            true,   // Motorcycle_observed_flow = 15,
            true,   // Bicycle_observed_flow = 16,
            true,   // Pedestrian_observed_flow_across_the_road = 17,
            true,   // Pedestrian_observed_flow_along_the_road_driver_side = 18,
            true,   // Pedestrian_observed_flow_along_the_road_passenger_side = 19,
            true,   // Land_use_driver_side =20,
            true,   // Land_use_passenger_side = 21,
            true,   // Area_type = 22,
            true,   // Speed_limit = 23,
            true,   // Motorcycle_speed_limit = 24,
            true,   // Truck_speed_limit = 25,
            true,   // Differential_speed_limits = 26,
            true,   // Median_type = 27,
            true,   // Centreline_rumble_strips = 28,
            true,   // Roadside_severity_driver_side_distance = 29,
            true,   // Roadside_severity_driver_side_object = 30,
            true,   // Roadside_severity_passenger_side_distance = 31,
            true,   // Roadside_severity_passenger_side_object = 32,
            true,   // Shoulder_rumble_strips = 33,
            true,   // Paved_shoulder_driver_side = 34,
            true,   // Paved_shoulder_passenger_side = 35,
            true,   // Intersection_type = 36,
            true,   // Intersection_channelisation = 37,
            true,   // Intersecting_road_volume = 38,
            true,   // Intersection_quality = 39,
            true,   // Property_access_points = 40,
            true,   // Number_of_lanes = 41,
            true,   // Lane_width = 42,
            true,   // Curvature = 43,
            true,   // Quality_of_curve = 44,
            true,   // Grade = 45,
            true,   // Road_condition = 46,
            true,   // Skid_resistance = 47,
            true,   // Delineation = 48,
            true,   // Street_lighting = 49,
            true,   // Pedestrian_crossing_facilities_inspected_road = 50,
            true,   // Pedestrian_crossing_quality = 51,
            true,   // Pedestrian_crossing_facilities_intersecting_road = 52,
            true,   // Pedestrian_fencing = 53,
            true,   // Speed_management_traffic_calming = 54,
            true,   // Vehicle_parking = 55,
            true,   // Sidewalk_driver_side = 56,
            true,   // Sidewalk_passenger_side = 57,
            true,   // Service_road = 58,
            true,   // Facilities_for_motorised_two_wheelers = 59,
            true,   // Facilities_for_bicycles = 60,
            true,   // Roadworks = 61,
            true,   // Sight_distance = 62,
            false,  // Vehicle_flow_AADT = 63,
            true,   // Motorcycle_PERCENT = 64,
            true,   // Pedestrian_peak_hour_flow_across_the_road = 65,
            true,   // Pedestrian_peak_hour_flow_along_the_road_driver_side = 66,
            true,   // Pedestrian_peak_hour_flow_along_the_road_passenger_side = 67,
            true,   // Bicycle_peak_hour_flow = 68,
            true,   // Operating_Speed_85th_percentile = 69,
            true,   // Operating_Speed_mean = 70,
            true,   // Roads_that_cars_can_read = 71,
            true,   // Vehicle_occupant_star_rating_policy_target = 72,
            true,   // Motorcycle_star_rating_Policy_target = 73,
            true,   // Pedestrian_star_rating_Policy_target = 74,
            true,   // Bicycle_star_rating_policy_target = 75,
            false,  // Annual_fatality_growth_multiplier = 76,
            true,   // School_zone_warning = 77,
            true,   // School_zone_crossing_supervisor = 78

        };

        public void DumpTableToFile(SqlConnection connection, string tableName, string destinationFile, int startVal, int endVal)
        {
            string totalNum = (endVal - startVal + 1).ToString();
            string skipNum = startVal.ToString();

            using (var command = new SqlCommand("select * from " + tableName + " ORDER BY Id OFFSET " + skipNum + " ROWS FETCH NEXT " + totalNum + " ROWS ONLY", connection))
            using (var reader = command.ExecuteReader())
            using (var outFile = File.CreateText(destinationFile))
            {
                string[] columnNames = GetColumnNames(reader).ToArray();
                outFile.WriteLine(string.Join(",", columnNames.Skip(totalColumnToSkip)));

                int numFields = columnNames.Length - totalColumnToSkip;
              
                Console.WriteLine(numFields);
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        /*
                         * string[] columnValues =
                            Enumerable.Range(totalColumnToSkip, numFields)
                                      .Select(i => reader.GetValue(i).ToString())
                                      .ToArray();
                        */

                        string[] columnValues = new string[numFields];

                        for (int i = 0; i < numFields; i++)
                        {
                            if (trimValue[i])
                            {
                                string[] trimed = reader.GetValue(i + totalColumnToSkip).ToString().Split('-');
                                columnValues[i] = trimed[0].Trim();
                            }
                            else
                            {
                                columnValues[i] = reader.GetValue(i + totalColumnToSkip).ToString();
                            }
                            
                        }
                        outFile.WriteLine(string.Join(",", columnValues));
                    }
                }
            }
        }
        private IEnumerable<string> GetColumnNames(IDataReader reader)
        {
            foreach (DataRow row in reader.GetSchemaTable().Rows)
            {
                yield return (string)row["ColumnName"];
            }
        }
    }
}
