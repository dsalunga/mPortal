// add ExtAuthDB
// MySQL Host Name.
$wgExtAuthDB_MySQL_Host = '';
// MySQL Username.
$wgExtAuthDB_MySQL_Username = '';
// MySQL Password.
$wgExtAuthDB_MySQL_Password = '';
// MySQL Database Name.
$wgExtAuthDB_MySQL_Database = '';
// MySQL Database Table of users data.
$wgExtAuthDB_MySQL_Table = '';
// MySQL Database username column label.
$wgExtAuthDB_MySQL_Login = '';
// MySQL Database login password column label
$wgExtAuthDB_MySQL_Pswrd = '';
// MySQL Database email column label
$wgExtAuthDB_MySQL_Email = '';
// MySQL Database user real name column label
$wgExtAuthDB_MySQL_RealN = '';
 
require_once("$IP/extensions/ExtAuthDB/ExtAuthWcmsPortal.php");
$wgAuth = new ExtAuthWcmsPortal();