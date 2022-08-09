"use strict";

var client = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();

$(function () {
    client.start().then(function () {
        window.setInterval((() => InvokeProducts()), 1000);	
    }).catch(function () {
		return console.error(err.toString());
    });

    client.on("ReceivedAll", function (data, order, reserve, rating, products, charts, time) {
        $('#user').find('h3').html(`${data}`);
        $('#order').find('h3').html(`${order}`);
        $('#reserve').find('h3').html(`${reserve}`);
        $('#rating').find('h3').html(`${Math.round(rating)}<sup style="font-size: 20px">%</sup>`);
        $('#time').html(`最後更新時間: ${time}`);
        BindProductsToGrid(products);
        BindProductsToChart(charts);
    });
});

function InvokeProducts() {
    client.invoke("GetAll").catch(function (err) {
        return console.error(err.toString());
    });
}
function BindProductsToGrid(products) {
	$('#tblProduct tbody').empty();

	var tr;
	$.each(products, function (index, product) {
		tr = $('<tr/>');
		tr.append(`<td>${(index + 1)}</td>`);
		tr.append(`<td>${product.productName}</td>`);
		tr.append(`<td>${product.stock}</td>`);
		$('#tblProduct').append(tr);
	});
}
function BindProductsToChart(chart) {
    const MONTHS = [
        '一月',
        '二月',
        '三月',
        '四月',
        '五月',
        '六月',
        '七月',
        '八月',
        '九月',
        '十月',
        '十一月',
        '十二月'
    ];

    function months(config) {
        var cfg = config || {};
        var section = cfg.section;
        var values = [];
        var value;

        for (let i = 0; i <6; i++) {
            value = MONTHS[moment().add(1-i, 'months').date(0).startOf('month').month()];
            values.push(value.substring(0, section));
        }

        return values.reverse();
    }

    var $Chart = $('#myChart');
    var labels = months({ count:6});
    var data = {
        labels: labels,
        datasets: [
            {
                label: "每月訂單數量",
                data: chart,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(255, 159, 64, 0.2)',
                    'rgba(255, 205, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(201, 203, 207, 0.2)'
                ],
                borderColor: [
                    'rgb(255, 99, 132)',
                    'rgb(255, 159, 64)',
                    'rgb(255, 205, 86)',
                    'rgb(75, 192, 192)',
                    'rgb(54, 162, 235)',
                    'rgb(153, 102, 255)',
                    'rgb(201, 203, 207)'
                ],
                borderWidth: 1
            }]
    };

    var salesChart = new Chart($Chart, {
        type: 'bar',
        data: data,
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            responsive: true,
            maintainAspectRatio: false
        }
    })
}