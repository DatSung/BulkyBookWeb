$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Admin/Products/GetAll' },
        "columns": [
            { data: 'productTitle', "width": "20%" },
            { data: 'isbn', "width": "20%" },
            { data: 'listPrice', "width": "15%" },
            { data: 'author', "width": "15%" },
            { data: 'category.categoryName', "width": "15%" }
        ]
    });
}