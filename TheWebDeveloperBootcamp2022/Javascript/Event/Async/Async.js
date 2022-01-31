


async function fakeRequestPromise() {
    try {
        console.log(await retrieveData2("www.dear.com"));
        console.log(await retrieveData2("www.dear.com/1"));
    } catch (error) {
        console.log("This is error1");
        console.log(error)
    }
}


function fakeRequestPromise2() {
    retrieveData2("www.dear.com")
        .then((data) => { 
        console.log(data);
        return retrieveData2("www.dear.com/1");
    })
        .then((data) => {
            console.log(data)
        })
        .catch((err) => {
        console.log("this is error3")
        console.log(err)
        })

}


const retrieveData2 = function (url) {
    return new Promise((resolve, reject) => {
        const delay = Math.floor(Math.random() * 4500) + 500;
        setTimeout((() => {
            if (delay <= 4000) {
                console.log(delay);
                return resolve(`Here is your data from ${url}`)
                //we will put data inside resolve, so that .then can pick
                //it up
            }
            else {
                return reject("Connection Time out!");
            }
        }), delay)
    })
};


fakeRequestPromise2();
console.log("This is the end.")