$(function () {
    Exit();
    deleteTalk();
})


function Exit() {
    $("#exit").click(function () {
        if (confirm("你确认退出吗？")) {
            return true;
        }
        else {
            return false;
        }
    })
}

function deleteTalk() {
    $(".deleteTalk").click(function () {
        if (confirm("确认删除吗？")) {
            return true;
        }
        else {
            return false;
        }
    })
}