

function getChartJs(type, deger) {


    var arrayDizi = deger.split('&');


    var config = null;
    if (type === 'line') {
        config = {
            type: 'line',
            data: {
                labels: ["Bilgisayar1", "Bilgisayar2", "Bilgisayar3", "Bilgisayar4", "Bilgisayar5", "Bilgisayar6"],
                datasets: [{
                    label: "Ram Kullanýmý",
                    data: [10, 20],
                        borderColor: 'rgba(0, 188, 212, 0.75)',
                        backgroundColor: 'rgba(0, 188, 212, 0.3)',
                        pointBorderColor: 'rgba(0, 188, 212, 0)',
                        pointBackgroundColor: 'rgba(0, 188, 212, 0.9)',
                        pointBorderWidth: 1
                },
                    {
                        label: "Youtube",
                        data: [10, 20],
                        borderColor: 'rgba(233, 30, 99, 0.75)',
                        backgroundColor: 'rgba(233, 30, 99, 0.3)',
                        pointBorderColor: 'rgba(233, 30, 99, 0)',
                        pointBackgroundColor: 'rgba(233, 30, 99, 0.9)',
                        pointBorderWidth: 1
                },
                    {
                        label: "Youtube",
                        data: [10, 20],
                        borderColor: 'rgba(0, 109, 204, 1)',
                        backgroundColor: 'rgba(0, 109, 204, 0.78)',
                        pointBorderColor: 'rgba(0, 109, 204, 1)',
                        pointBackgroundColor: 'rgba(0, 109, 204, 1)',
                        pointBorderWidth: 1
                },
                    {
                        label: "Youtube",
                        data: [10,20],
                        borderColor: 'rgba(228, 0, 4, 1)',
                        backgroundColor: 'rgba(230, 0, 4, 0.85)',
                        pointBorderColor: 'rgba(228, 0, 4, 1)',
                        pointBackgroundColor: 'rgba(228, 0, 4, 1)',
                        pointBorderWidth: 1
                }
                ]
            },
            options: {
                responsive: true,
                legend: false
            }
        }
    }



    else if (type === 'bar') {
        config = {
            type: 'bar',
            data: {
                labels: arrayDizi[0].split(','),
                datasets: [{
                    label: "RAM KULLANIMI",
                    data: arrayDizi[2].split(','),
                    backgroundColor: 'rgba(0, 188, 212, 0.8)'
                }, {
                        label: "DISK KULLANIMI",
                        data: arrayDizi[1].split(','),
                        backgroundColor: 'rgba(233, 30, 99, 0.8)'
                        }, {
                            label: "ISLEMCI KULLANIMI",
                            data: arrayDizi[3].split(','),
                            backgroundColor: 'rgb(0, 98, 184)'
                                }]
            },
            options: {
                responsive: true,
                legend: false
            }
        }
    }
    else if (type === 'pie') {

        var dizi = deger.split(',');

        config = {
            type: 'pie',
            data: {
                datasets: [{
                    data: dizi,
                    backgroundColor: [
                        "rgb(228, 0, 4)",
                        "rgb(0, 188, 212)"
                    ],
                }],
                labels: [
                    "Dolu",
                    "Bos"
                ]
            },
            options: {
                responsive: true,
                legend: false
            }
        }
    }

    else if (type === 'pie2') {
        var dizi2 = deger.split(',');

        config = {
            type: 'pie',
            data: {
                datasets: [{
                    data: dizi2,
                    backgroundColor: [
                        "rgb(228, 0, 4)",
                        "rgb(0, 188, 212)"
                    ],
                }],
                labels: [
                    "Dolu",
                    "Bos"
                ]
            },
            options: {
                responsive: true,
                legend: false
            }
        }
    }

    else if (type === 'pie3') {
        var dizi2 = deger;
        var bosBolum = 100 - dizi2;
        var sonuc = dizi2 + "," + bosBolum;
        var diziSon = sonuc.split(',');

        config = {
            type: 'pie',
            data: {
                datasets: [{
                    data: diziSon,
                    backgroundColor: [
                        "rgb(228, 0, 4)",
                        "rgb(0, 188, 212)"
                    ],
                }],
                labels: [
                    "Dolu",
                    "Bos"
                ]
            },
            options: {
                responsive: true,
                legend: false
            }
        }
    }


    else if (type === 'radar') {
        config = {
            type: 'radar',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July"],
                datasets: [{
                    label: "My First dataset",
                    data: [65, 25, 90, 81, 56, 55, 40],
                    borderColor: 'rgba(0, 188, 212, 0.8)',
                    backgroundColor: 'rgba(0, 188, 212, 0.5)',
                    pointBorderColor: 'rgba(0, 188, 212, 0)',
                    pointBackgroundColor: 'rgba(0, 188, 212, 0.8)',
                    pointBorderWidth: 1
                }, {
                        label: "My Second dataset",
                        data: [72, 48, 40, 19, 96, 27, 100],
                        borderColor: 'rgba(233, 30, 99, 0.8)',
                        backgroundColor: 'rgba(233, 30, 99, 0.5)',
                        pointBorderColor: 'rgba(233, 30, 99, 0)',
                        pointBackgroundColor: 'rgba(233, 30, 99, 0.8)',
                        pointBorderWidth: 1
                    }]
            },
            options: {
                responsive: true,
                legend: false
            }
        }
    }
    return config;
}