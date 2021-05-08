"use strict";
exports.__esModule = true;
var server_1 = require("./server");
var http_1 = require("http");
var config_1 = require("./util/config");
var httpServer = http_1["default"].createServer(server_1.router);
httpServer.listen(config_1["default"].server.port, function () { return console.log("Server running on " + config_1["default"].server.hostname + ":" + config_1["default"].server.port); });
