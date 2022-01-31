const fakeRequestCallBack = function (url, success, failure) {
    const delay = Math.floor(Math.random() * 4500) + 500;
    setTimeout(() => {
        if (delay <= 4000) {
            failure("Connection Timeout!")
        }
        else {
            success(`Here is your data from ${url}`);
        }
    }, delay);
};

const fakeRequestPromise = function (url) {
    return new Promise((resolve, reject) => {
        const delay = Math.floor(Math.random() * 4500) + 500;
        setTimeout((() => {
            if (delay <= 4000) {
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


fakeRequestPromise("www.dear.com")
    .then((data) => {
    console.log("This work!");
    console.log(data);
    return fakeRequestPromise("www.dear.com/1");
})
    .then((data) => {
        console.log("This work2!");
        console.log(data);
        return fakeRequestPromise("www.dear.com/2");
    })
    .then((data) => {
        console.log("This work3!");
        console.log(data);
    })
    .catch((err) => {
        console.log(err);
    })

    