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
            url: "/Admin/Product/GetData",
            type: "GET",

            data: function (d) {

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
                data: "imgPath",
                orderable: false,
                searchable: false,
                width: "80px",
                render: function (img) {

                    const image = img
                        ? `/${img}`
                        : "/images/Default/defaultProduct.jpg";

                    return `
                        <img
                            src="${image}"
                            class="rounded shadow-sm"
                            style="width:60px;height:60px;object-fit:cover;"
                            alt="Product"/>
                    `;
                }
            },

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

                    return data.length > 60
                        ? data.substring(0, 60) + "..."
                        : data;
                }
            },

            {
                data: "price",
                className: "text-center",
                render: function (price) {

                    return `
                        <span class="badge bg-success fs-6">
                            $${Number(price).toFixed(2)}
                        </span>
                    `;
                }
            },

            {
                data: "categoryName",
                className: "text-center",
                render: function (category) {

                    return `
                        <span class="badge bg-primary">
                            ${category}
                        </span>
                    `;
                }
            },

            {
                data: "id",
                orderable: false,
                searchable: false,
                className: "text-center",
                width: "150px",

                render: function (id) {

                    return `

                        <div class="btn-group" role="group">

                            <a href="/Admin/Product/Edit/${id}"
                               class="btn btn-outline-primary btn-sm"
                               title="Edit">

                                <i class="fa-solid fa-pen"></i>

                            </a>

                            <button
                                onclick="Delete('/Admin/Product/Delete/${id}')"
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
            searchPlaceholder: "Search products...",

            lengthMenu: "Show _MENU_ products",

            info: "Showing _START_ to _END_ of _TOTAL_ products",
                
            infoEmpty: "No products found",

            zeroRecords: "No matching products found",

            processing: "Loading..."
        }

    });

});
