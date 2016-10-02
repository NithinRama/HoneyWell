var express = require('express');
var app = express();
var mysql      = require('mysql');
var conn = mysql.createConnection({
  host     : 'localhost',
  user     : 'root',
  password : 'sandbox123',
  database : 'honeywell'
});
var _ = require('underscore');
var bodyParser = require('body-parser');
var async = require('async');

// conn.query('SELECT * FROM Location', function(err, res){
//     if(err)
//         return console.log(err);
//     console.log(res);
//         return;
// });

// dynamically load all locations and save in the memory

var locs ={};

conn.query('SELECT * FROM Location', function(err, res){
    if(err)
        console.log('Failed to fetch locations');
    else
        locs = res;
})

app.use(bodyParser.json());

// registration apis

app.post('/api/location/mobile', function(req, res){
    var data = {};
    data.email = req.body.email;
    data.name = req.body.name;
    data.imei = req.body.imei;
    var q = 'INSERT INTO DetailsMobile SET ? ';
    conn.query(q, data, function(err, result){
        if(err)
        {
            res.send(err);
            return console.log(err);
        }
        res.send(result);
        return console.log(result);
    });
});

// app.post('/api/location/laptop', function(req, res){
//     var data = {};
//     data.email = req.body.email;
//     data.name = req.body.name;
//     data.deviceMac = req.body.mac;
//     var q = 'INSERT INTO DetailsLaptop SET ? ';
//     conn.query(q, data, function(req, result){
//         if(err)
//         {
//             res.send(err);
//             return console.log(err);
//         }
//         return console.log(result);
//     });
// });

//

// test queries here 

// var q = 'SELECT * FROM DetailsMobile WHERE email = ?';
// conn.query(q, 'omiyurulz@gmail.com', function(err, result){
//     if(err)
//         return console.log(err);
//     if(result.length == 0)
//         console.log('IM EMPTY!');
//     return console.log(result);
// });

//

// get by email

app.get('/api/location/email/:email', function(req, res){

    var q1 = function(callback){
        var q = 'SELECT * FROM DetailsMobile WHERE email = ? ';
        conn.query(q, req.params.email, function(err, result){
            if(err)
                return callback(err, null);
            return callback(null, result);
        });
    };

    var q2 = function(callback){
        var q = 'SELECT * FROM DetailsLaptop WHERE email = ? ';
        conn.query(q, req.params.email, function(err, result){
            if(err)
                return callback(err, null);
            return callback(null, result);
        });
    }

    async.parallel([q1, q2], function(err, result){
        if(err)
            return res.send(err);
        
        var resData = {};
        for(var i=0;i<result[0].length; i++)
        {
            result[0][i].location1 = _.findWhere(locs, {id: result[0][i].location1}) || null;
            result[0][i].location2 = _.findWhere(locs, {id: result[0][i].location2}) || null;
            result[0][i].location3 = _.findWhere(locs, {id: result[0][i].location3}) || null;
            !!result[0][i].time1 ? result[0][i].time1 = result[0][i].time1.toString() : result[0][i].time1 = null;
            !!result[0][i].time2 ? result[0][i].time2 = result[0][i].time2.toString() : result[0][i].time2 = null;
            !!result[0][i].time3 ? result[0][i].time3 = result[0][i].time3.toString() : result[0][i].time3 = null;
        }
        for(var i=0;i<result[1].length; i++)
        {
            result[1][i].location1 = _.findWhere(locs, {id: result[1][i].location1}) || null;
            result[1][i].location2 = _.findWhere(locs, {id: result[1][i].location2}) || null;
            result[1][i].location3 = _.findWhere(locs, {id: result[1][i].location3}) || null;
            console.log('---');
            console.log(result[1][i]);
            console.log('---');
            !!result[1][i].time1 ? result[1][i].time1 = result[1][i].time1.toString() : result[1][i].time1 = null ;
            !!result[1][i].time2 ? result[1][i].time2 = result[1][i].time2.toString() : result[1][i].time2 = null;
            !!result[1][i].time3 ? result[1][i].time3 = result[1][i].time3.toString() : result[1][i].time3 = null;
        }
        resData.mobile = result[0];
        resData.laptop = result[1];
        res.send(resData);     
    });
});


//

// get by name

app.get('/api/location/name/:name', function(req, res){

    var q = 'SELECT DISTINCT email FROM DetailsLaptop WHERE name = ? UNION SELECT email FROM DetailsMobile WHERE name = ?';
    conn.query(q, [req.params.name, req.params.name], function(err, result){
        if(err){
            res.send(err);
            return console.log(err);
        }
        res.send(result);
    });

});

//


// app.get('/api/location/email/:email', function(req, res){
//     var q = 'SELECT id,name,location1,time1,location2,time2,location3,time3 FROM Details WHERE email = ?';
//     conn.query(q,req.params.email, function(err, result){
//         if(err){
//             res.send(err);
//             return console.log(err);
//         }

//         result[0].location1 = _.findWhere(locs, {id: result[0].location1});
//         result[0].location2 = _.findWhere(locs, {id: result[0].location2});
//         result[0].location3 = _.findWhere(locs, {id: result[0].location3});
//         result[0].time1 = result[0].time1.toString();
//         result[0].time2 = result[0].time2.toString();
//         result[0].time3 = result[0].time3.toString();        
//         console.log(result[0].time1);        
//         res.send(result[0]);
//     });
// });

// app.get('/api/location/name/:name', function(req, res){
//     var q = 'SELECT id,name,location1,time1,location2,time2,location3,time3 FROM Details WHERE name = ?';
//     console.log(req.params.name);
//     conn.query(q,req.params.name, function(err, result){
//         if(err){
//             res.send(err);
//             return console.log(err);
//         }
//         for(var i=0; i<result.length;i++){
//             result[i].location1 = _.findWhere(locs, {id: result[i].location1});
//             result[i].time1 = result[i].time1.toString();
//             result[i].location2 = _.findWhere(locs, {id: result[i].location2});
//             result[i].time2 = result[i].time2.toString();
//             result[i].location3 = _.findWhere(locs, {id: result[i].location3});
//             result[i].time3 = result[i].time3.toString();
//         }
//         console.log(result);
//         res.send(result);
//     });
// })

app.put('/api/location/mobile/:email', function(req, res){
    
    var q = 'UPDATE DetailsMobile SET location3 = location2, time3=time2, location2 = location1, time2=time1, location1 = ?, time1 = NOW() WHERE email = ?';
    var loc = _.findWhere(locs, {routerMac: req.body.mac});
    if(!loc)
        return res.status(404).send('Invalid Mac');
    loc = loc.id;
    conn.query(q,[loc, req.params.email], function(err, result){
        if(err){
            res.send(err);
            return console.log(err);
        }
        res.send(result);
    });
});

app.put('/api/location/laptop/email/:email', function(req, res){
    var q = 'UPDATE DetailsLaptop SET location3 = location2, time3=time2, location2 = location1, time2=time1, location1 = ?, time1 = NOW() WHERE email = ?';
    var loc = _.findWhere(locs, {routerMac: req.body.mac});
    if(!loc)
        return res.status(404).send('Invalid Mac');
    loc = loc.id;
    conn.query(q,[loc, req.params.email], function(err, result){
        if(err){
            res.send(err);
            return console.log(err);
        }
        res.send(result);
    });
});

app.put('/api/location/laptop/mac/:deviceMac', function(req, res){
    console.log('inside the put request. Mac = ' + req.body.mac);
    var q = 'UPDATE DetailsLaptop SET location3 = location2, time3=time2, location2 = location1, time2=time1, location1 = ?, time1 = NOW() WHERE deviceMac = ?';
    var loc = _.findWhere(locs, {routerMac: req.body.mac});
    if(!loc)
        return res.status(404).send('Invalid Mac');
    loc = loc.id;
    conn.query(q,[loc, req.params.deviceMac], function(err, result){
        if(err){
            res.send(err);
            return console.log(err);
        }
        res.send(result);
    });
});

app.listen(3000, console.log('Successfully hosted at port 3000'));


/*

POST REQUESTS: For registration

/api/location/mobile
    email
    name
    imei

GET REQUESTS: To get location by email or get email by name

/api/location/email/:email
    returns:
        {
            mobile: [ { id: , name: , imei: , location1: , time1: , location2: , time2: , location3: , time3: } ],
            laptop: [ { id: , name: , imei: , location1: , time1: , location2: , time2: , location3: , time3: } ]
        }
/api/location/name/:name
    returns:
        [ { email: } ]

PUT REQUESTS: To update the current location

/api/location/mobile/:email
    body:
        mac
*/

