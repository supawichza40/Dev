//We are going to create a program that generate folder with 3 files with the same name
//1.html
//2.css
//3.javascript

//example of command node javascriptBoilerplate.js pathOfFolder nameOfFolder

const fs = require("fs");

const folderName = process.argv[2] || "Project";
console.log("lol", folderName)
const pathExample = "C:\\Work\Dev\TheWebDeveloperBootcamp2022\Javascript\NodeJS";
const bashPathExample = pathExample.replace('\\', "//")
console.log(bashPathExample);
console.log(pathExample);
console.log(process.argv[0], process.argv[1])
console.log(`${pathExample} \ ${folderName}`)