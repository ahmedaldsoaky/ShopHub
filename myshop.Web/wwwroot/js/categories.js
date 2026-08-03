console.log("categories.js loaded");

$(function () {

    $('#mytable').DataTable({

        processing: true,
        serverSide: true,
        responsive: false,
        autoWidth: false,

        pageLength: 10,
        lengthMenu: [10, 25, 50, 100],

        order: [[0, "asc"]],

        ajax: {
            url: "/Admin/Category/GetData",
            type: "GET",

            data: function (d) {

                console.log("DataTables Request:", d);

                d.pageNumber = (d.start / d.length) + 1;
                d.pageSize = d.length;

                d.search = d.search.value;

                d.sortColumn = d.columns[d.order[0].column].data;
                d.sortDirection = d.order[0].dir;

            },

            dataSrc: function (json) {

                console.log("Response:", json);

                return json.data;
            }
        },

        columns: [

            {
                data: "name",
                render: function (data) {
                    return `<strong>${data}</strong>`;
                }
            },

            {
                data: "description",
                render: function (data) {

                    if (!data)
                        return "-";

                    return data.length > 70
                        ? data.substring(0, 70) + "..."
                        : data;
                }
            },

            {
                data: "createdTime",
                className: "text-center",
                render: function (data) {

                    return data
                        ? new Date(data).toLocaleDateString()
                        : "N/A";
                }
            },

            {
                data: "id",
                orderable: false,
                searchable: false,
                className: "text-center",
                width: "130px",

                render: function (id) {

                    return `
                        <div class="btn-group">

                            <a href="/Admin/Category/Edit/${id}"
                               class="btn btn-outline-primary btn-sm"
                               title="Edit">

                                <i class="fa-solid fa-pen"></i>

                            </a>

                            <button
                                onclick="Delete('/Admin/Category/Delete/${id}')"
                                class="btn btn-outline-danger btn-sm"
                                title="Delete">

                                <i class="fa-solid fa-trash"></i>

                            </button>

                        </div>
                    `;
                }
            }

        ],

        language: {

            search: "_INPUT_",
            searchPlaceholder: "Search categories...",

            lengthMenu: "Show _MENU_ categories",

            info: "Showing _START_ to _END_ of _TOTAL_ categories",

            infoEmpty: "No categories found",

            zeroRecords: "No matching categories found",

            processing: "Loading..."
        }

    });

});