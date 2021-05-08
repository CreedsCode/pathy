import bodyParser, { json, urlencoded } from "body-parser";
import express from "express";

export const router = express();

router.use(urlencoded({extended:false}));
router.use(json());

router.use((req, res, next) => {
    res.header("Access-Control-Allow-Origin", "*") // TODO: REEEEE
    res.header("Access-Control-Allow-Headers", "Origin, x-Request-With, Content-Type, Accept, Authorization");
    next();
});

// Error handling like a king
router.use((req, res, next) => {
    const error = new Error("not found, lol");

    return res.status(404).json({
        message: error.message,
        note: "Big ooof",
        details: "https://www.youtube.com/watch?v=dQw4w9WgXcQ"
    });
});



