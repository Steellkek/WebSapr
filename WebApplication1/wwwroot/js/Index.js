async function  graph(){
    console.log(5);
    const response = await fetch("/api/Graph/graph", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    
    const graph =  await response.json();
    console.log(graph)
    let cy = cytoscape({
    
        container: document.getElementById('cy'), // container to render in

        elements: [],

        style: [
            {
                selector: 'node',
                css: {
                    'content': 'data(id)',
                    'text-valign': 'center',
                    'text-halign': 'center',
                    'text-outline-width': 2,
                    'text-outline-color': '#888',
                }
            },
            {
                selector: ':parent',
                css: {
                    'text-valign': 'top',
                    'text-halign': 'center',
                    'color': "black"
                }
            },
            {
                selector: 'edge',
                css: {
                    'width': 2,
                    'line-color': '#369',
                    'target-arrow-color': '#369',
                    'target-arrow-shape': 'triangle',
                    'label': 'data(label)',
                    'font-size': '18px',
                    'color': '#ff0033'
                }
            }
        ],
    
        style: cytoscape.stylesheet()
            .selector('edge')
            .css({
                'width': 2,
                'line-color': '#369',
                'target-arrow-color': '#369',
                'target-arrow-shape': 'triangle',
                'label': 'data(label)',
                'font-size': '18px',
                'color': '#ff0033'
            })
            .selector('node')
            .css({
                'content': 'data(id)',
                'text-valign': 'center',
                'text-halign': 'center',
                'color': 'white',
                'text-outline-width': 2,
                'text-outline-color': '#888',
            })
            .selector(':selected')
            .css({
                'background-color': 'black',
                'line-color': 'black',
                'target-arrow-color': 'black',
                'source-arrow-color': 'black',
                'text-outline-color': 'black'
            })  
    });
    graph.vertexs.forEach(((vertex)=>{
        cy.add([{ group: 'nodes',data: { id: 'v'+vertex.number }, position: { x: 0, y: 0} }])

    }))
    var l=0;
    graph.edges.forEach(((edge)=>{
        console.log(edge)
        cy.add([{ group: 'edges',data: { id: 'e'+l, source: 'v'+edge.v1.number, target: 'v'+edge.v2.number, label: edge.weight} }])
        l+=1;
    }))

    cy.layout({ name: 'random' }).run();
    cy.layout({ name: 'random' }).stop();
    cy.on('click', 'node', function(evt){
        var node = evt.target;
        console.clear()
        console.log( node.position() );
    });
}


async function  gengraph(){
    const response = await fetch("/api/Graph/gengraph", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    const genalg =  await response.json();
    console.log(genalg)

    let cy = cytoscape({

        container: document.getElementById('cy'), // container to render in

            elements: [],

        style: [
            {
                selector: 'node',
                css: {
                    'content': 'data(id)',
                    'text-valign': 'center',
                    'text-halign': 'center',
                    'text-outline-width': 2,
                    'text-outline-color': '#888',
                }
            },
            {
                selector: ':parent',
                css: {
                    'text-valign': 'top',
                    'text-halign': 'center',
                    'color': "black"
                }
            },
            {
                selector: 'edge',
                css: {
                    'width': 2,
                    'line-color': '#369',
                    'target-arrow-color': '#369',
                    'target-arrow-shape': 'triangle',
                    'label': 'data(label)',
                    'font-size': '18px',
                    'color': '#ff0033'
                }
            }
        ],

        style: cytoscape.stylesheet()
            .selector('edge')
            .css({
                'width': 2,
                'line-color': '#369',
                'target-arrow-color': '#369',
                'target-arrow-shape': 'triangle',
                'label': 'data(label)',
                'font-size': '18px',
                'color': '#ff0033'
            })
            .selector('node')
            .css({
                'content': 'data(id)',
                'text-valign': 'center',
                'text-halign': 'center',
                'color': 'white',
                'text-outline-width': 2,
                'text-outline-color': '#888',
            })
            .selector(':selected')
            .css({
                'background-color': 'black',
                'line-color': 'black',
                'target-arrow-color': 'black',
                'source-arrow-color': 'black',
                'text-outline-color': 'black'
            })
    });

    for (let i = 0; i < genalg.split.length; i++) { // выведет 0, затем 1, затем 2
        cy.add([{group:'nodes',data:{id: 'x'+(i+1)}}])
    }
    var m=1;
    var n=0;
    genalg.gen.forEach(((vertex)=>{
        cy.add([{ group: 'nodes',data: { id: "v"+vertex.number, parent: 'x'+m }}])
        console.log('e'+m)
        n+=1;
        if (genalg.split[m-1]==n){
            n=0;
            m+=1;
        }
    }))

    var l=0;
    genalg._graph.edges.forEach(((edge)=>{
        console.log(edge)
        cy.add([{ group: 'edges',data: { id: 'e'+l, source: 'v'+edge.v1.number, target: 'v'+edge.v2.number, label: edge.weight} }])
        l+=1;
    }))

    cy.layout({ name: 'random' }).run();
    cy.layout({ name: 'random' }).stop();
    cy.on('click', 'node', function(evt){
        var node = evt.target;
        console.clear()
        console.log( evt.target.id );
    });
}

async function  newgraph(){
    console.log(4)

    const response = await fetch("/api/Graph/new", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    var graph = await response.json();
    console.log(graph)

    let cy = cytoscape({

        container: document.getElementById('cy'), // container to render in

        elements: [],

        style: [
            {
                selector: 'node',
                css: {
                    'content': 'data(id)',
                    'text-valign': 'center',
                    'text-halign': 'center',
                    'text-outline-width': 2,
                    'text-outline-color': '#888',
                }
            },
            {
                selector: ':parent',
                css: {
                    'text-valign': 'top',
                    'text-halign': 'center',
                    'color': "black"
                }
            },
            {
                selector: 'edge',
                css: {
                    'width': 2,
                    'line-color': '#369',
                    'target-arrow-color': '#369',
                    'target-arrow-shape': 'triangle',
                    'label': 'data(label)',
                    'font-size': '18px',
                    'color': '#ff0033'
                }
            }
        ],

        style: cytoscape.stylesheet()
            .selector('edge')
            .css({
                'width': 2,
                'line-color': '#369',
                'target-arrow-color': '#369',
                'target-arrow-shape': 'triangle',
                'label': 'data(label)',
                'font-size': '18px',
                'color': '#ff0033'
            })
            .selector('node')
            .css({
                'content': 'data(id)',
                'text-valign': 'center',
                'text-halign': 'center',
                'color': 'white',
                'text-outline-width': 2,
                'text-outline-color': '#888',
            })
            .selector(':selected')
            .css({
                'background-color': 'black',
                'line-color': 'black',
                'target-arrow-color': 'black',
                'source-arrow-color': 'black',
                'text-outline-color': 'black'
            })
    });
    graph.vertexs.forEach(((vertex)=>{
        cy.add([{ group: 'nodes',data: { id: 'v'+vertex.number }, position: { x: 0, y: 0} }])

    }))
    var l=0;
    graph.edges.forEach(((edge)=>{
        console.log(edge)
        cy.add([{ group: 'edges',data: { id: 'e'+l, source: 'v'+edge.v1.number, target: 'v'+edge.v2.number, label: edge.weight} }])
        l+=1;
    }))

    cy.layout({ name: 'random' }).run();
    cy.layout({ name: 'random' }).stop();
    cy.on('click', 'node', function(evt){
        var node = evt.target;
        console.clear()
        console.log( node.position() );
    });
}
