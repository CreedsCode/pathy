"use strict";
exports.__esModule = true;
exports.router = void 0;
var body_parser_1 = require("body-parser");
var express_1 = require("express");
exports.router = express_1["default"]();
exports.router.use(body_parser_1.urlencoded({ extended: false }));
exports.router.use(body_parser_1.json());
exports.router.use(function (req, res, next) {
    res.header("Access-Control-Allow-Origin", "*"); // TODO: REEEEE
    res.header("Access-Control-Allow-Headers", "Origin, x-Request-With, Content-Type, Accept, Authorization");
    next();
});
// Error handling like a king
exports.router.use(function (req, res, next) {
    var error = new Error("not found, lol");
    return res.status(404).json({
        message: error.message,
        note: "Big ooof",
        details: "https://www.youtube.com/watch?v=dQw4w9WgXcQ"
    });
});
