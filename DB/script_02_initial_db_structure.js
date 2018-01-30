db = connect('hashhunters');
db.auth('hhadmin', 'ifx342vfns');
db.createCollection('users', {autoIndexId: true});
