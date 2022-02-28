const express = require('express')
const app = express()
const port = 1234;


app.get('/home', (req, res) => {
    res.send("<h1>tHIS IS MY WEBPAGE11</h1>")
})
app.get('/s/:animalName', (req, res) => {
    const { animalName } = req.params
    const animalName2 = req.params
    console.log(req.params)
    res.send(`<h1>This is a ${animalName} ${animalName2.animalName} 2</h1>`)
})

app.listen(port, () => {
    console.log(`Example app listening on port ${port}`)
})