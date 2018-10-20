$(document).ready(function () {
        
    var ds = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/api/individualapi/LoadIndividual",
                contentType: "application/json; charset=utf-8",
                type: "GET" },
            create: function (persona) {
                var item = persona.data;
                item.id = counter++;
                persona.success(item);
            },
            update: function (persona) { persona.success(); },
            destroy: function (persona) { persona.success(); }
        },
        schema: {
            model: {
                id: "RecordNumber",
                fields: {
                    FirstName: { validation: { required: true } },
                    LastName: { validation: { required: false } },
                    Gender: { validation: { required: true } },
                    Addresses: { validation: { required: true } }
                }
            }
        },
        change: function (e) {
        }
    });
    
    var list = [];


    function onChange(e) {
        list = [];
        var rows = e.sender.select();
        rows.each(function (e) {
            var grid = $("#persons").data("kendoGrid");
            var dataItem = grid.dataItem(this);
            list.push(dataItem);
        })
    
    };

    $("#persons").kendoGrid({
        dataSource: ds,
        pageable: true,
        scrollable: false,
        persistSelection: true,
        sortable: true,
        change: onChange,
        autoBind: true,
        columns: [{
            selectable: true,
            width: "50px"
        },
        {
            field: "FirstName",
            title: "FirstName"
        },
        {
            field: "LastName",
            title: "LastName"
        },
        {
            field: "Gender",
            title: "Gender"
        },
        {
            field: "addresses",
            title:"Addresses"
        }
        ],
        toolbar: [
            {
                name: "Add",
                text: "Merge Customers",
                click: function (e) { console.log("foo"); return false; }
            }
        ]
    });

    $(".k-grid-Add", "#persons").bind("click", function (ev) {
        if (list.length > 2) {
            alert("Solo debe seleccionar dos personas");
            
        } else {
            var persons = [];
            var ind = 0;
            list.forEach(function (e) {
                persons[ind] = e.id;
                ind++;
               
            })

            var url = "/api/individualapi/Merge?firstPerson=" + persons[0] + "&secondPerson=" + persons[1];
            $.ajax({
                url: url,
                contentType: "application/json; charset=utf-8",
                type: "GET",
                data: JSON.stringify({}),
                complete: function (jqXhr, textStatus) {
                }

            });
        }
        //ds.read();
    });

    ds.read();

});