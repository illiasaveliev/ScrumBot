$(function () {
    var uri = 'api/sprints';

    $.getJSON(uri)
        .done(function (data) {
            $.each(data, function (key, item) {
                $('<tr><td>' + (key + 1) + '</td><td>' + item.name + '</td><td>' + item.teamName + '</td></tr>')
                    .appendTo($('#sprints tbody'));
            });
        });
});
