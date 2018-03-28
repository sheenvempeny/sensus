// read in files (TODO: support async)
var fs = require("fs");
var push_notifications_content = fs.readFileSync("push-notifications.json");
var push_notifications_local_content = fs.readFileSync("push-notifications-local.json");

var push_notifications = JSON.parse(push_notifications_content);
var push_notifications_local = JSON.parse(push_notifications_local_content);

console.log("\n-----------------------REMOTE NOTIFICATIONS-------------------------\n");
// console.log(push_notifications);

// get the array of remote notifications
if(push_notifications.hasOwnProperty('push_notifications')){
        var remote_notifs_arr = push_notifications.push_notifications;
        console.log(remote_notifs_arr);
} else {
        console.log("no key found.\n"); //TODO: error handling
}

console.log("\n-----------------------LOCAL NOTIFICATIONS-------------------------\n");
//console.log(push_notifications_local);

// get the array of local notifications
if(push_notifications_local.hasOwnProperty('push_notifications')){
        var local_notifs_arr = push_notifications_local.push_notifications;
        console.log(local_notifs_arr);
} else {
        console.log("no key found.\n"); //TODO: error handling
}

// add non-dups to new_notifications array
var new_notifs_arr = [];

// NOTE: assumes local and remote are identical besides new entries at bottom
for(var r = local_notifs_arr.length; r < remote_notifs_arr.length; r++){
        new_notifs_arr.push(remote_notifs_arr[r]);
}

console.log("\n------------------------NEW NOTIFICATIONS-------------------------\n");
console.log(new_notifs_arr);

// TODO: administer the notifications via REST request

for (var n = 0; n < new_notifs_arr.length; n++){
        if(new_notifs_arr[n].hasOwnProperty('participant') && new_notifs_arr[n].hasOwnProperty('message')){
                var device_id = new_notifs_arr[n].participant;
                var message = new_notifs_arr[n].message;
                console.log("\nSending notification with:\ndevice id:" + device_id + "\nmessage:" + message);
        }
}
