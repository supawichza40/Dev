const clickMeBtn = document.querySelector("button");
const ul = document.querySelector("ul");

clickMeBtn.onmouseenter = function () {
    const li = document.createElement("li");
    li.append("Mouse Enter the button");
    ul.append(li);
};
clickMeBtn.onmouseleave = function () {
    const li = document.createElement("li");
    li.append("Mouse exit the button");
    ul.append(li);
};

