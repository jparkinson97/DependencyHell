window.initializeGraph = (elements) => {
    var cy = cytoscape({
        container: document.getElementById('cy'), // The HTML container ID

        elements: elements, // The data coming from C#

        style: [
            // Parent Nodes (Assemblies)
            {
                selector: 'node[type="assembly"]',
                style: {
                    'background-color': '#f0f0f0',
                    'label': 'data(label)',
                    'shape': 'roundrectangle',
                    'color': '#333',
                    'font-weight': 'bold',
                    'text-valign': 'top',
                    'text-halign': 'center'
                }
            },
            // Child Nodes (Types)
            {
                selector: 'node[type="type"]',
                style: {
                    'background-color': '#512BD4', // MAUI Purple
                    'label': 'data(label)',
                    'color': '#fff',
                    'font-size': '12px',
                    'text-valign': 'center',
                    'text-halign': 'center',
                    'width': 'label',
                    'height': 'label',
                    'padding': '8px',
                    'shape': 'roundrectangle'
                }
            },
            // Edges (Dependencies)
            {
                selector: 'edge',
                style: {
                    'width': 2,
                    'line-color': '#999',
                    'target-arrow-color': '#999',
                    'target-arrow-shape': 'triangle',
                    'curve-style': 'bezier'
                }
            }
        ],

        layout: {
            name: 'dagre',
            rankDir: 'LR', // Left to Right layout
            padding: 50
        }
    });
};