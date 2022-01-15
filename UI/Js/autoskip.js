onload = function () {
    setInterval(go, 1000);
};
var x = 6; //利用了全局变量来执行


function go() {   
    if (x > 0) {
        document.getElementById("sp").innerHTML = x; //每次设置的x的值都不一样了。
    } else {
        location.href = '/Home/Index';
    }
    x--;
}
