$(function () {
    //Widgets count
    $('.count-to').countTo();

    //Sales count to
    $('.sales-count-to').countTo({
        formatter: function (value, options) {
            return '$' + value.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, ' ').replace('.', ',');
        }
    });

    initRealTimeChart();

    initDonutChart3();
    initDonutChart4();
    initDonutChart5();
    initDonutChart6();
    initDonutChart7();
    initDonutChart8();
    initDonutChart10();
    initDonutChart11();
    initRealTimeChart();
    initDonutChart();
    initDonutChart2();


    initSparkline();
});

var realtime = 'on';
function initRealTimeChart() {
    //Real time ==========================================================================================
    var plot = $.plot('#real_time_chart', [getRandomData()], {
        series: {
            shadowSize: 0,
            color: 'rgb(0, 188, 212)'
        },
        grid: {
            borderColor: '#f3f3f3',
            borderWidth: 1,
            tickColor: '#f3f3f3'
        },
        lines: {
            fill: true
        },
        yaxis: {
            min: 0,
            max: 100
        },
        xaxis: {
            min: 0,
            max: 100
        }
    });

    var plot2 = $.plot('#real_time_chart2', [getRandomData2()], {
    	series: {
    		shadowSize: 0,
    		color: 'rgb(0, 188, 212)'
    	},
    	grid: {
    		borderColor: '#f3f3f3',
    		borderWidth: 1,
    		tickColor: '#f3f3f3'
    	},
    	lines: {
    		fill: true
    	},
    	yaxis: {
    		min: 0,
    		max: 100
    	},
    	xaxis: {
    		min: 0,
    		max: 100
    	}
    });

    var plot3 = $.plot('#real_time_chart3', [getRandomData3()], {
    	series: {
    		shadowSize: 0,
    		color: 'rgb(0, 188, 212)'
    	},
    	grid: {
    		borderColor: '#f3f3f3',
    		borderWidth: 1,
    		tickColor: '#f3f3f3'
    	},
    	lines: {
    		fill: true
    	},
    	yaxis: {
    		min: 0,
    		max: 100
    	},
    	xaxis: {
    		min: 0,
    		max: 100
    	}
    });

    function updateRealTime() {
        plot.draw();
		plot2.draw();
		plot3.draw();

    }

    updateRealTime();


    //====================================================================================================
}

function initSparkline() {
    $(".sparkline").each(function () {
        var $this = $(this);
        $this.sparkline('html', $this.data());
    });
}

function initDonutChart() {
    Morris.Donut({
        element: 'donut_chart',
        data: [{
            label: 'Foto',
            value: 450
        }, {
            label: 'Video',
            value: 300
        }, {
            label: 'Metin',
            value: 102
        }, {
            label: 'Url',
            value: 12
        }],
        colors: ['rgb(255, 152, 0)', 'rgb(0, 150, 136)', 'rgb(233, 30, 99)', 'rgb(33, 150, 243)'],
        formatter: function (y) {
            return y + ''
        }
    });
}

function initDonutChart2() {
    Morris.Donut({
        element: 'donut_chart_2',
        data: [{
            label: 'RT',
            value: 450
        }, {
            label: 'Like',
            value: 300
            }, {
                label: 'Mention',
                value: 300
            }],

        colors: ['rgb(255, 152, 0)', 'rgb(233, 30, 99)', 'rgb(33, 150, 243)'],
        formatter: function (y) {
            return y + ''
        }
    });
}

function initDonutChart3() {
    Morris.Donut({
        element: 'donut_chart_3',
        data: [{
            label: 'Foto',
            value: 450
        }, {
            label: 'Video',
            value: 300
        }, {
            label: 'Metin',
            value: 102
        }, {
            label: 'Url',
            value: 12
        }],
        colors: ['rgb(255, 152, 0)', 'rgb(0, 150, 136)', 'rgb(233, 30, 99)', 'rgb(33, 150, 243)'],
        formatter: function (y) {
            return y + ''
        }
    });
}

function initDonutChart4() {
    Morris.Donut({
        element: 'donut_chart_4',
        data: [{
            label: 'Beğeni',
            value: 450
        }, {
            label: 'Paylaşım',
            value: 300
        }, {
            label: 'Yorum',
            value: 300
        }],

        colors: ['rgb(255, 152, 0)', 'rgb(233, 30, 99)', 'rgb(33, 150, 243)'],
        formatter: function (y) {
            return y + ''
        }
    });
}

function initDonutChart5() {
    Morris.Donut({
        element: 'donut_chart_5',
        data: [{
            label: 'Olumlu',
            value: 450
        }, {
            label: 'Olumsuz',
            value: 300
        }],

        colors: ['rgb(76, 175, 80)', 'rgb(228, 0, 4)'],
        formatter: function (y) {
            return y + ''
        }
    });
}

function initDonutChart6() {
    Morris.Donut({
        element: 'donut_chart_6',
        data: [{
            label: 'Foto',
            value: 450
        }, {
            label: 'Video',
            value: 300
        }],
        colors: ['rgb(255, 152, 0)', 'rgb(0, 150, 136)'],
        formatter: function (y) {
            return y + ''
        }
    });
}

function initDonutChart7() {
    Morris.Donut({
        element: 'donut_chart_7',
        data: [{
            label: 'Beğeni',
            value: 450
        }, {
            label: 'Yorum',
            value: 300
        }],

        colors: ['rgb(255, 152, 0)', 'rgb(233, 30, 99)'],
        formatter: function (y) {
            return y + ''
        }
    });
}

function initDonutChart8() {
    Morris.Donut({
        element: 'donut_chart_8',
        data: [{
            label: 'Olumlu',
            value: 450
        }, {
            label: 'Olumsuz',
            value: 300
        }],

        colors: ['rgb(76, 175, 80)', 'rgb(228, 0, 4)'],
        formatter: function (y) {
            return y + ''
        }
    });
}

function initDonutChart10() {
    Morris.Donut({
        element: 'donut_chart_10',
        data: [{
            label: 'Beğeni',
            value: 450
        }, {
            label: 'Yorum',
            value: 300
        }],

        colors: ['rgb(255, 152, 0)', 'rgb(233, 30, 99)'],
        formatter: function (y) {
            return y + ''
        }
    });
}

function initDonutChart11() {
    Morris.Donut({
        element: 'donut_chart_11',
        data: [{
            label: 'Olumlu',
            value: 450
        }, {
            label: 'Olumsuz',
            value: 300
        }],

        colors: ['rgb(76, 175, 80)', 'rgb(228, 0, 4)'],
        formatter: function (y) {
            return y + ''
        }
    });
}


var datasx = document.getElementById("tablolar").value.split('&');


var data = datasx[0], totalPoints = 60;


function getRandomData() {
    if (data.length > 0) data = data.split(',');

    while (data.length < totalPoints) {
        var prev = data.length > 0 ? data[data.length - 1] : 50, y = prev + Math.random() * 10 - 5;
        if (y < 0) { y = 0; } else if (y > 100) { y = 100; }

        data.push(y);
    }

    var res = [];
    for (var i = 0; i < data.length; ++i) {
        res.push([i, data[i]]);
    }

    return res;
}


var data2 = datasx[1];

function getRandomData2() {
	if (data2.length > 0) data2 = data2.split(',');

	while (data2.length < totalPoints) {
		var prev = data2.length > 0 ? data2[data2.length - 1] : 50, y = prev + Math.random() * 10 - 5;
		if (y < 0) { y = 0; } else if (y > 100) { y = 100; }

		data2.push(y);
	}

	var res = [];
	for (var i = 0; i < data2.length; ++i) {
		res.push([i, data2[i]]);
	}

	return res;
}


var data3 = datasx[2];

function getRandomData3() {
	if (data3.length > 0) data3 = data3.split(',');

	while (data3.length < totalPoints) {
		var prev = data3.length > 0 ? data3[data3.length - 1] : 50, y = prev + Math.random() * 10 - 5;
		if (y < 0) { y = 0; } else if (y > 100) { y = 100; }

		data3.push(y);
	}

	var res = [];
	for (var i = 0; i < data3.length; ++i) {
		res.push([i, data3[i]]);
	}

	return res;
}