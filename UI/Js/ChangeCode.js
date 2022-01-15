$(function () {
    CCode();
})

function CCode() {
    $(".code").click(function () {
        var date = new Date();

        $(".code").attr("src", "/User/ValidateCode/" + date.getMilliseconds());
    })
}