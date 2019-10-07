var WebTemplate = WebTemplate || {};

WebTemplate.EmbeddedDataTables = function () {

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

    var moveDataTablesDom = function (tableId) {
        $('#' + tableId + '_paginate').detach().appendTo('.wt-sub-navbar .datatable_actions');
        $('#' + tableId + '_info').detach().appendTo('.wt-sub-navbar .datatable_actions');

        var filter = $('#' + tableId + '_filter input').detach();
        filter.attr('id', 'wt-search-control');
        var filterContainer = $('#' + tableId + '_filter');
        filterContainer.html('');
        filter.appendTo(filterContainer);

        var searchLocation = '.wt-filter .wt-search';
        if ($('.wt-filter .wt-search').length === 0) {
            searchLocation = '.wt-sub-navbar .datatable_actions';
            $(searchLocation).append('<label id="wt-search-label" for="wt-search-control">Search</label>');
        }
        filterContainer.detach().appendTo(searchLocation);
    }

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
            dom: "iptrf",
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

        moveDataTablesDom(tableId);
        addFilterChangeEvents();
    }

    var ajax = function (tableId, url, columns, options) {
        if (options === undefined) options = {};
        options = $.extend(true, defaults, options);

        dataTable = $('#' + tableId).DataTable({
            dom: "iptrf",
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

        moveDataTablesDom(tableId);
        addFilterChangeEvents();
    }

    return {
        ClientSide: clientSide,
        Ajax: ajax,
        UpdateTable: updateTable
    };
}();