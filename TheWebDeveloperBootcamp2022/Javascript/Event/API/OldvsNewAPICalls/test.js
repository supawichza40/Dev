class Car{
    
    constructor(model, name) {
        this.model = model;
        this.name = name;
    }
    description = function () {
        console.log(`${this.model} ${this.name}`);
    }
    description2() {
        console.log("This is a descripton")
    }

}
const car1 = new Car("BM", "Supercar");
console.log(car1.model);
car1.description();
car1.description2();