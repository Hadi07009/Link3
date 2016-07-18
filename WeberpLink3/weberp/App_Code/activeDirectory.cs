using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for activeDirectory
/// </summary>

public class AdUsers
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public bool isMapped { get; set; }
}

public enum objectClass
{
    user, group, computer
}
public enum returnType
{
    distinguishedName, ObjectGUID
}

public class activeDirectory
{

    string servername = "203.76.97.86";
    //string domainAndUsername = "MOBS-BD" + @"\" + "Administrator";
    string domainAndUsername = "Administrator@MOBS-BD.COM";
    string pwd = "BxH69<>?";



    public activeDirectory()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public bool IsAuthenticate(string userName,  string password)
    {
        bool authentic = false;
        try
        {
            string dname = System.Configuration.ConfigurationSettings.AppSettings["dname"].ToString();
            string dip = System.Configuration.ConfigurationSettings.AppSettings["dip"].ToString();

            //IsAuthenticate(TextBox4.Text + "@MOBS-BD.COM", TextBox5.Text)
           // DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain,  userName, password);
            DirectoryEntry entry = new DirectoryEntry("LDAP://" + dip, userName + "@" + dname, password);
            object nativeObject = entry.NativeObject;
            authentic = true;
        }
        catch (DirectoryServicesCOMException) { }
        return authentic;
    }

    public void AddToGroup(string userDn, string groupDn)
    {
        try
        {
            DirectoryEntry dirEntry = new DirectoryEntry("LDAP://" + groupDn);
            dirEntry.Properties["member"].Add(userDn);
            dirEntry.CommitChanges();
            dirEntry.Close();
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException E)
        {
            //doSomething with E.Message.ToString();

        }
    }

    public void RemoveUserFromGroup(string userDn, string groupDn)
    {
        try
        {
            DirectoryEntry dirEntry = new DirectoryEntry("LDAP://" + groupDn);
            dirEntry.Properties["member"].Remove(userDn);
            dirEntry.CommitChanges();
            dirEntry.Close();
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException E)
        {
            //doSomething with E.Message.ToString();

        }
    }

    public ArrayList Groups()
    {
        ArrayList groups = new ArrayList();
        foreach (System.Security.Principal.IdentityReference group in
            System.Web.HttpContext.Current.Request.LogonUserIdentity.Groups)
        {
            groups.Add(group.Translate(typeof
                (System.Security.Principal.NTAccount)).ToString());
        }
        return groups;
    }

    public ArrayList Groups(string userDn, bool recursive)
    {
        ArrayList groupMemberships = new ArrayList();
        return AttributeValuesMultiString("memberOf", userDn,
            groupMemberships, recursive);
    }

    public ArrayList AttributeValuesMultiString(string attributeName, string objectDn, ArrayList valuesCollection, bool recursive)
    {
        DirectoryEntry ent = new DirectoryEntry(objectDn);
        PropertyValueCollection ValueCollection = ent.Properties[attributeName];
        IEnumerator en = ValueCollection.GetEnumerator();

        while (en.MoveNext())
        {
            if (en.Current != null)
            {
                if (!valuesCollection.Contains(en.Current.ToString()))
                {
                    valuesCollection.Add(en.Current.ToString());
                    if (recursive)
                    {
                        AttributeValuesMultiString(attributeName, "LDAP://" +
                        en.Current.ToString(), valuesCollection, true);
                    }
                }
            }
        }
        ent.Close();
        ent.Dispose();
        return valuesCollection;
    }

    public string AttributeValuesSingleString (string attributeName, string objectDn)
    {
        string strValue;
        DirectoryEntry ent = new DirectoryEntry(objectDn);
        strValue = ent.Properties[attributeName].Value.ToString();
        ent.Close();
        ent.Dispose();
        return strValue;
    }

   


    public void Enable(string userDn)
    {
        try
        {
            DirectoryEntry user = new DirectoryEntry(userDn);
            int val = (int)user.Properties["userAccountControl"].Value;
            user.Properties["userAccountControl"].Value = val & ~0x2;
            //ADS_UF_NORMAL_ACCOUNT;

            user.CommitChanges();
            user.Close();
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException E)
        {
            //DoSomethingWith --> E.Message.ToString();

        }
    }

    public void Disable(string userDn)
    {
        try
        {
            DirectoryEntry user = new DirectoryEntry(userDn);
            int val = (int)user.Properties["userAccountControl"].Value;
            user.Properties["userAccountControl"].Value = val | 0x2;
            //ADS_UF_ACCOUNTDISABLE;

            user.CommitChanges();
            user.Close();
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException E)
        {
            //DoSomethingWith --> E.Message.ToString();

        }
    }

    public void Unlock(string userDn)
    {
        try
        {
            DirectoryEntry uEntry = new DirectoryEntry(userDn);
            uEntry.Properties["LockOutTime"].Value = 0; //unlock account

            uEntry.CommitChanges(); //may not be needed but adding it anyways

            uEntry.Close();
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException E)
        {
            //DoSomethingWith --> E.Message.ToString();

        }
    }

    //public bool IsLocked
    //{
    //    get { return Convert.ToBoolean(dEntry.InvokeGet("IsAccountLocked")); }
    //    set { dEntry.InvokeSet("IsAccountLocked", value); }
    //}

    public void ResetPassword(string userDn, string password)
    {
        DirectoryEntry uEntry = new DirectoryEntry(userDn);
        uEntry.Invoke("SetPassword", new object[] { password });
        uEntry.Properties["LockOutTime"].Value = 0; //unlock account

        uEntry.Close();
    }

    public static void Rename(string objectDn, string newName)
    {
        DirectoryEntry child = new DirectoryEntry("LDAP://" + objectDn);
        child.Rename("CN=" + newName);
    }

    public List<AdUsers> GetADUsers()
    {

     
        DirectoryEntry searchRoot = new DirectoryEntry("LDAP://" + servername, domainAndUsername, pwd);

            List<AdUsers> lstADUsers = new List<AdUsers>();
            
            //DirectoryEntry searchRoot = new DirectoryEntry(DomainPath);
            DirectorySearcher search = new DirectorySearcher(searchRoot);
            search.Filter = "(&(objectClass=user)(objectCategory=person))";
            search.PropertiesToLoad.Add("samaccountname");
            search.PropertiesToLoad.Add("mail");
            search.PropertiesToLoad.Add("usergroup");
            search.PropertiesToLoad.Add("displayname");//first name
            SearchResult result;
            SearchResultCollection resultCol = search.FindAll();
            if (resultCol != null)
            {
                for (int counter = 0; counter < resultCol.Count; counter++)
                {
                    string UserNameEmailString = string.Empty;
                    result = resultCol[counter];
                    if (result.Properties.Contains("samaccountname") &&
                             result.Properties.Contains("mail") &&
                        result.Properties.Contains("displayname"))
                    {
                        AdUsers objSurveyUsers = new AdUsers();
                        objSurveyUsers.Email = (String)result.Properties["mail"][0];
                        objSurveyUsers.UserName = (String)result.Properties["samaccountname"][0];
                        objSurveyUsers.DisplayName = (String)result.Properties["displayname"][0];
                        lstADUsers.Add(objSurveyUsers);
                    }
                }
            }
            return lstADUsers;
   
    }

    public List<string> GetGroups()
    {

       
        DirectoryEntry objADAM = new DirectoryEntry("LDAP://" + servername, domainAndUsername, pwd);
        // Binding object. 
        DirectoryEntry objGroupEntry = new DirectoryEntry("LDAP://" + servername, domainAndUsername, pwd);
        // Group Results. 
        DirectorySearcher objSearchADAM = default(DirectorySearcher);
        // Search object. 
        SearchResultCollection objSearchResults = default(SearchResultCollection);
        // Results collection. 
        string strPath = null;
        // Binding path. 
        List<string> result = new List<string>();

        // Construct the binding string. 

       
        
        strPath = "LDAP://" + servername;
        //Change to your ADserver 

        // Get the AD LDS object. 
        try
        {
            objADAM = new DirectoryEntry("LDAP://" + servername, domainAndUsername, pwd);
            objADAM.RefreshCache();
        }
        catch (Exception e)
        {
            throw e;
        }

        // Get search object, specify filter and scope, 
        // perform search. 
        try
        {
            objSearchADAM = new DirectorySearcher(objADAM);
            objSearchADAM.Filter = "(&(objectClass=group))";
            objSearchADAM.SearchScope = SearchScope.Subtree;
            objSearchResults = objSearchADAM.FindAll();
        }
        catch (Exception e)
        {
            throw e;
        }

        // Enumerate groups 
        try
        {
            if (objSearchResults.Count != 0)
            {
                foreach (SearchResult objResult in objSearchResults)
                {
                    objGroupEntry = objResult.GetDirectoryEntry();
                    result.Add(objGroupEntry.Name);
                }
            }
            else
            {
                throw new Exception("No groups found");
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return result;
    }

    public List<string> GetComputers()
    {


        DirectoryEntry enTry = new DirectoryEntry("LDAP://" + servername, domainAndUsername, pwd);
    DirectorySearcher mySearcher =new DirectorySearcher(enTry);
     List<string> result = new List<string>();

    mySearcher.Filter = "(&(objectClass=computer))";
    //SearchResult resEntAs;

        foreach(SearchResult resEnt in  mySearcher.FindAll())
        {
            result.Add(resEnt.GetDirectoryEntry().Name.ToString());
        }

        return result;
    
    }


    public string CreateUserAccount(string userName, string userPassword)
    {
        string oGUID = string.Empty;
        try
        {
                        
            
            DirectoryEntry dirEntry = new DirectoryEntry("LDAP://" + servername, domainAndUsername, pwd);
            DirectoryEntry newUser = dirEntry.Children.Add
                ("CN=" + userName, "user");
            newUser.Properties["samAccountName"].Value = userName;
            newUser.CommitChanges();
            oGUID = newUser.Guid.ToString();

            newUser.Invoke("SetPassword", new object[] { userPassword });
            newUser.CommitChanges();
            dirEntry.Close();
            newUser.Close();
        }
        catch (System.DirectoryServices.DirectoryServicesCOMException E)
        {
            oGUID = E.Message.ToString();

        }
        return oGUID + " Created";
    }

    public string AddAdUser(string username, string firstname, string lastname)
    {
        DirectoryEntry objADAM; // Binding object.
        DirectoryEntry objUser; // User object.

      

        string strDisplayName = firstname + " " + lastname;
       // string strUser = username;
        string strUserPrincipalName = username + "@MOBS-BD.COM";
        //string pw = "abc123";
       // string strGroup = "CN=DC,OU=Groups";
       // string samacct;


        //const long ADS_OPTION_PASSWORD_PORTNUMBER = 6;
        //const long ADS_OPTION_PASSWORD_METHOD = 7;
        //const int ADS_PASSWORD_ENCODE_REQUIRE_SSL = 0;
        //const int ADS_PASSWORD_ENCODE_CLEAR = 1;

        //string strServer = "10.x.x.x"; //IP to your ad server
        //string strUserOu = "OU=My Domain Users,DC=coryclough,DC=info";
        //string strPort = "389";
        //int intPort = Int32.Parse(strPort);

       // AuthenticationTypes AuthTypes = AuthenticationTypes.Signing | AuthenticationTypes.Sealing | AuthenticationTypes.Secure;

        try
        {
            objADAM = new DirectoryEntry("LDAP://" + servername, domainAndUsername, pwd);
            objADAM.RefreshCache();
        }
        catch (Exception e)
        {
            return e.Message;
        }

        try
        {
            //Make sure same account is 20 chars or less
            if (username.Length > 19)
            {
                username = username.Substring(0, 19);
            }
            

            //Create new user
            objUser = objADAM.Children.Add("CN=" + username + ",ou=MIS", "user");
            objUser.Properties["displayName"].Add(strDisplayName);
            objUser.Properties["userPrincipalName"].Add(strUserPrincipalName);
            objUser.Properties["mail"].Add(strUserPrincipalName);
            objUser.Properties["sAMAccountName"].Add(username);
            objUser.Properties["givenName"].Add(firstname);
            objUser.Properties["sn"].Add(lastname);
            objUser.CommitChanges();
            objUser.RefreshCache();

            //Set default password
            //objUser.Invoke("SetOption", new object[] { ADS_OPTION_PASSWORD_PORTNUMBER, intPort });
            //objUser.Invoke("SetOption", new object[] { ADS_OPTION_PASSWORD_METHOD, ADS_PASSWORD_ENCODE_CLEAR });
            objUser.Invoke("SetPassword", new object[] { pwd });
            objUser.CommitChanges();
            objUser.RefreshCache();

            //Enable account and change password on first logon flag
            objUser.Properties["userAccountControl"].Value = 0x200;
            objUser.Properties["pwdLastSet"].Value = 0;
            objUser.CommitChanges();
            objUser.RefreshCache();

        }
        catch (Exception e)
        {
            return e.Message;
        }

        //Add user to students group
        //try
        //{
        //    DirectoryEntry dom = new DirectoryEntry("LDAP://" + servername, domainAndUsername, pwd);
        //    DirectoryEntry group = dom.Children.Find(strGroup);
        //    group.Properties["member"].Add("CN=" + username + ",OU=MIS,OU=My Domain Users,DC=coryclough,DC=info");
        //    group.CommitChanges();
        //}
        //catch (Exception e)
        //{
        //    return e.Message;
        //}

        return "done";
    }

}



