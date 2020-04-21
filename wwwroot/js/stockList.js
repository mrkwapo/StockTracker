var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/stocks/getall/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "symbol", "width": "20%" },
            { "data": "last_Price", "width": "20%" },
            { "data": "change", "width": "20%" },
            { "data": "change_Percentage", "width": "20%" },
            { "data": "currency", "width": "20%" },
            { "data": "market_Time", "width": "20%" },
            { "data": "volume", "width": "20%" },
            { "data": "average_Volume", "width": "20%" },
            { "data": "market_Cap", "width": "20%" },
            { "data": "scrapeDate", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Stocks/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                            </a>
                            &nbsp;
                            <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete('/stocks/Delete?id='+${data})>
                            Delete
                            </a>
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}