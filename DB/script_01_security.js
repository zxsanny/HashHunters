db = connect('admin');
db.createUser(
{
	user: "admin", 
	pwd: "ifx342vfns",
	roles:[{ role: "userAdminAnyDatabase", db: "admin"}]
});

db = connect('hashhunters');
db.createUser(
{
	user: 'hhadmin',
	pwd:'ifx342vfns',
	roles:[{role:'dbOwner', db: 'hashhunters'}]
});
