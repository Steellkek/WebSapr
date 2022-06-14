async function  graph(){
    document.getElementById("graph").disabled = true;
    document.getElementById("gengraph").disabled = true;
    document.getElementById("newgraph").disabled = true;
    
    const response = await fetch("/api/Graph/graph", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok)
    {
        const graph = await response.json();
        let cy = window.cy = cytoscape({

            container: document.getElementById('cy'), 
 
            elements: [],
            

            style: cytoscape.stylesheet()
                .selector('edge')
                .css({
                    'width': 1,
                    'line-color': 'Black',
                    'target-arrow-color': 'black',
                    'target-arrow-shape': 'triangle',
                    'label': 'data(label)',
                    'font-size': '30px',
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
        graph.vertexs.forEach(((vertex) => {
            cy.add([{group: 'nodes', data: {id: 'v' + vertex.number}, position: {x: 0, y: 0}}])

        }))
        var NumberOfEdge = 0;
        graph.edges.forEach(((edge) => {
            cy.add([{
                group: 'edges',
                data: {id: 'e' + NumberOfEdge, source: 'v' + edge.v1.number, target: 'v' + edge.v2.number, label: edge.weight}
            }])
            NumberOfEdge += 1;
        }))
        cy.layout({name: 'circle'}).run();
        cy.layout({name: 'circle'}).stop();

        document.getElementById("CountGenome").innerText = "";
        document.getElementById("Iteration").innerText = "";
        document.getElementById("ChanseCrosover").innerText = "";
        document.getElementById("ChanseMutation").innerText = "";
        document.getElementById("ChanseInversion").innerText = "";
        document.getElementById("CF").innerText = "";
        document.getElementById("NeedSplit").innerText = "";
        document.getElementById("Split").innerText = "";
        document.getElementById("TimeBestGen").innerText = "";
        document.getElementById("Time").innerText = "";
    }
    else{
        alert("проверьте файл с данными!")
    }
    document.getElementById("graph").disabled = false;
    document.getElementById("gengraph").disabled = false;
    document.getElementById("newgraph").disabled = false;
}

async function  gengraph(){
    document.getElementById("graph").disabled = true;
    document.getElementById("gengraph").disabled = true;
    document.getElementById("newgraph").disabled = true;
    const response = await fetch("/api/Graph/gengraph", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok)
    {
        try 
        {
            const genalg = await response.json();
            console.log(genalg)
            
            let cy = cytoscape({

                container: document.getElementById('cy'),

                elements: [],

                style: cytoscape.stylesheet()
                    .selector('edge')
                    .css({
                        'width': 1,
                        'line-color': 'Black',
                        'target-arrow-color': 'black',
                        'target-arrow-shape': 'triangle',
                        'label': 'data(label)',
                        'font-size': '30px',
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
            for (let i = 0; i < genalg.bestGen.split.length; i++) {
                cy.add([{group: 'nodes', data: {id: 'g' + (i + 1)}}])
            }
            var Split = "|";
            var m = 1;
            var n = 0;
            genalg.bestGen.gen.forEach(((vertex) => {
                cy.add([{group: 'nodes', data: {id: "v" + vertex.number, parent: 'g' + m}}])
                Split += "v" + vertex.number + " "
                n += 1;
                if (genalg.bestGen.split[m - 1] == n) {
                    n = 0;
                    m += 1;
                    Split += "|"
                }
            }))

            var NumberOfEdge = 0;
            genalg._graph.edges.forEach(((edge) => {
                cy.add([{
                    group: 'edges',
                    data: {id: 'e' + NumberOfEdge, source: 'v' + edge.v1.number, target: 'v' + edge.v2.number, label: edge.weight}
                }])
                NumberOfEdge += 1;
            }))

            cy.layout({name: 'circle'}).run();
            cy.layout({name: 'circle'}).stop();

            document.getElementById("CountGenome").innerText = genalg.countGenome;
            document.getElementById("Iteration").innerText = genalg.iteration;
            document.getElementById("ChanseCrosover").innerText = genalg.chanseCrosover;
            document.getElementById("ChanseMutation").innerText = genalg.chanseMutation;
            document.getElementById("ChanseInversion").innerText = genalg.chanseInversion;
            document.getElementById("CF").innerText = genalg.bestGen.fitness;
            document.getElementById("NeedSplit").innerText = genalg.bestGen.split.join(" ");
            document.getElementById("Split").innerText = Split;
            document.getElementById("TimeBestGen").innerText = genalg.bestGen.time + " мс";
            document.getElementById("Time").innerText = genalg.time + " мс";
        }catch (err){
            alert("Генетический алгоритм не нашел решение, попробуйте увеличить число особей или поколений!")
        }
    }
    else {
        alert("Проверьте файл с данными!!!")
    }

    document.getElementById("graph").disabled = false;
    document.getElementById("gengraph").disabled = false;
    document.getElementById("newgraph").disabled = false;
}

async function  newgraph(){
    document.getElementById("graph").disabled = true;
    document.getElementById("gengraph").disabled = true;
    document.getElementById("newgraph").disabled = true;
    if ((document.getElementById("number").value>4)&&(document.getElementById("number").value<200)&&(Number.isInteger(Number(document.getElementById("number").value)))) {
        {
            result = confirm("Нынешний граф и разбиение будут удалены. Продолжаем?")
            if (result) {
                const response = await fetch("/api/Graph/"+Number(document.getElementById("number").value), {
                    method: "GET",
                    headers: {"Accept": "application/json"}
                });
                if (response.ok)
                {
                    var graph = await response.json();

                    let cy = cytoscape({

                        container: document.getElementById('cy'),

                        elements: [],

                        style: cytoscape.stylesheet()
                            .selector('edge')
                            .css({
                                'width': 1,
                                'line-color': 'Black',
                                'target-arrow-color': 'black',
                                'target-arrow-shape': 'triangle',
                                'label': 'data(label)',
                                'font-size': '30px',
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

                    graph.vertexs.forEach(((vertex) => {
                        cy.add([{group: 'nodes', data: {id: 'v' + vertex.number}, position: {x: 0, y: 0}}])

                    }))
                    var NumberOfEdge = 0;
                    graph.edges.forEach(((edge) => {
                        cy.add([{
                            group: 'edges',
                            data: {
                                id: 'e' + NumberOfEdge,
                                source: 'v' + edge.v1.number,
                                target: 'v' + edge.v2.number,
                                label: edge.weight
                            }
                        }])
                        NumberOfEdge += 1;
                    }))

                    cy.layout({name: 'circle'}).run();
                    cy.layout({name: 'circle'}).stop();

                    document.getElementById("CountGenome").innerText = "";
                    document.getElementById("Iteration").innerText = "";
                    document.getElementById("ChanseCrosover").innerText = "";
                    document.getElementById("ChanseMutation").innerText = "";
                    document.getElementById("ChanseInversion").innerText = "";
                    document.getElementById("CF").innerText = "";
                    document.getElementById("NeedSplit").innerText = "";
                    document.getElementById("Split").innerText = "";
                    document.getElementById("TimeBestGen").innerText = "";
                    document.getElementById("Time").innerText = "";
                }
                else {
                    alert("Проверьте файл с данными!")
                }
            }
        }
    }
    else{
        alert("Введите целое число больше 4 и меньше 200")
    }
    document.getElementById("graph").disabled = false;
    document.getElementById("gengraph").disabled = false;
    document.getElementById("newgraph").disabled = false;
}

