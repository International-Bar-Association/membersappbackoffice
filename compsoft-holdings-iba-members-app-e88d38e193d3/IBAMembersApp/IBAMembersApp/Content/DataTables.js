var WebTemplate = WebTemplate || {};

WebTemplate.DataTables = function () {

    var defaults = {
        pageLength: 10,
        additionalData: function () {
            var data = {};
            $('#wt-filter-form input')
                .each(function () {
                    var input = $(this);
                    data[input.attr('name')] = input.val();
                });
            return data;
        },
        language: {
            emptyTable: "No items",
            processing: "Querying...",
            info: "_PAGE_ of _PAGES_"
        }
    }

    var dataTable;

    var updateTable = function () {
        if (dataTable) {
            dataTable.draw();
        }
    }

    var addFilterChangeEvents = function () {
        $('#wt-filter-form input').on("input", updateTable);
        $('#wt-filter-form input').on("change", updateTable);
    }

    var clientSide = function (tableId, options) {
        if (options === null || options === undefined) options = {};

        if (options.columns === undefined) options.columns=null;
        if (options.columnDefs === undefined) options.columnDefs = null;
        if (options.order === undefined) options.order = null;
        options = $.extend(true, defaults, options);

        dataTable = $('#' + tableId).DataTable({
            lengthChange : false,
            pagingType: "simple",
            pageLength: options.pageLength,
            language: {
                emptyTable: options.emptyTable,
                processing: options.processing,
                info: options.info
            },
            columns: options.columns,
            columnDefs: options.columnDefs,
            order:options.order
        });
        addFilterChangeEvents();
    }

    var ajax = function (tableId, url, columns, options) {
        if (options === undefined) options = {};
        options = $.extend(true, defaults, options);

        dataTable = $('#' + tableId).DataTable({
            lengthChange: false,
            pagingType: "simple",
            pageLength: options.pageLength,
            language: {
                emptyTable: options.emptyTable,
                processing: options.processing,
                info: options.info
            },
            serverSide: true,
            processing: true,
            ajax: {
                url: url,
                "type": "POST",
                data: function (data) {
                    if (options.additionalData) {
                        return $.extend({}, options.additionalData(), data);
                    }
                    return data;
                }
            },
            columns: columns
        });

        addFilterChangeEvents();
    }

    return {
        ClientSide: clientSide,
        Ajax: ajax,
        UpdateTable: updateTable
    };
}();