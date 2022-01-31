function testArg() {
    for (let val = 0; val < arguments.length; val++){
        console.log(arguments[val]);
    }
}
testArg(1, 2, 3, 4, 5, 6, 7);