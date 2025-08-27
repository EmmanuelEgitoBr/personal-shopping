var dataTable;

$(() => {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: "/Order/GetAllOrders",
            type: "GET"
        },
        "columns": [
            { data: "orderheaderid", "width":"5%" },
            { data: "email", "width":"25%" }
        ]
    })
}