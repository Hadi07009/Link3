using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for StringGenerator
/// </summary>
public class StringGenerator
{
    public StringGenerator()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string CreatingCommaSeparatedlist(string inputString1, string inputString2, string inputString3,
                                             string inputString4, string inputString5, string inputString6)
    {
        try
        {
            string commaSeparatedlist = null;
            string lastIndexValue = null;
            IList<string> stringList = new List<string>();
            if (inputString1 != null)
            {
                stringList.Add(inputString1);

            }

            if (inputString2 != null)
            {
                stringList.Add(inputString2);

            }

            if (inputString3 != null)
            {
                stringList.Add(inputString3);

            }

            if (inputString4 != null)
            {
                stringList.Add(inputString4);

            }

            if (inputString5 != null)
            {
                stringList.Add(inputString5);

            }

            if (inputString6 != null)
            {
                stringList.Add(inputString6);

            }

            int countIndex = stringList.Count;
            if (countIndex == 0)
            {
                return commaSeparatedlist;

            }

            if (countIndex == 1)
            {
                return commaSeparatedlist = string.Join(",", stringList);

            }

            if (countIndex == 2)
            {
                return commaSeparatedlist = string.Join(" and ", stringList);

            }

            if (countIndex > 2)
            {
                lastIndexValue = stringList[countIndex - 1].ToString();
                stringList.Remove(stringList.Last());
                commaSeparatedlist = string.Join(",", stringList);
                commaSeparatedlist = commaSeparatedlist + " and " + lastIndexValue;
            }

            return commaSeparatedlist;

        }
        catch (Exception msgException)
        {

            throw msgException;
        }
    }

    public string CommaSeparatedlistFromGridView(GridView gridViewID, string columnIndexs)
    {
        try
        {
            string commaSeparatedlist = null;
            string lastIndexValue = null;
            IList<string> stringList = new List<string>();
            foreach (GridViewRow rowNo in gridViewID.Rows)
            {
                String cellText = null;
                int cellIndex = 0;
                
                foreach (TableCell cell in rowNo.Cells)
                {
                    bool checkIndex = false;
                    string[] arryColumnIndex = columnIndexs.Split(',');
	                foreach (string selectedColumnIndex in arryColumnIndex)
	                {
                        if (cellIndex == Convert.ToInt32( selectedColumnIndex))
                        {
                            checkIndex = true;
                        
                        }
                    }

                    if (checkIndex == false)
                    {
                        cellIndex++;
                        continue;
                        
                    }

                    if (cell.Text == "&nbsp;")
                    {
                        cellIndex++;
                        continue;
                    }

                    if (cellIndex == (rowNo.Cells.Count - 1) )
                    {
                        cellText += " @";

                    }
                    cellText += cell.Text + " ";
                    cellIndex++;

                }
                stringList.Add(cellText);
            }

            int countIndex = stringList.Count;
            if (countIndex == 0)
            {
                return commaSeparatedlist;

            }

            if (countIndex == 1)
            {
                return commaSeparatedlist = string.Join(",", stringList);

            }

            if (countIndex == 2)
            {
                return commaSeparatedlist = string.Join(" and ", stringList);

            }

            if (countIndex > 2)
            {
                lastIndexValue = stringList[countIndex - 1].ToString();
                stringList.Remove(stringList.Last());
                commaSeparatedlist = string.Join(",", stringList);
                commaSeparatedlist = commaSeparatedlist + " and " + lastIndexValue;
            }

            return commaSeparatedlist;

        }
        catch (Exception msgException)
        {

            throw msgException;
        }

    }
}