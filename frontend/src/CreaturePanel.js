import React, {useState} from 'react'
import CreateCreature from './CreateCreature';

class CreaturePanel extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
          error: null,
          isLoaded: false,
          items: []
        };
      }

    getCreatures()
    {
        fetch("https://localhost:7131/Initiative/GetCreatures")
        .then(res => res.json())
        .then(
          (result) => {
            this.setState({
              isLoaded: true,
              items: result.items
            });
          },
          // Uwaga: to ważne, żeby obsłużyć błędy tutaj, a
          // nie w bloku catch(), aby nie przetwarzać błędów
          // mających swoje źródło w komponencie.
          (error) => {
            this.setState({
              isLoaded: true,
              error
            });
          }
        )
    }

    componentDidMount() {
        this.getCreatures();
      };

      render() {
        const { error, isLoaded, items } = this.state;
        const [ connection, setConnection ] = useState(null);

        useEffect(() => {
            const newConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:7131/hubs/initiative', {
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets
              })
            .withAutomaticReconnect()
            .build();
        
            setConnection(newConnection);
        }, []);
    
        useEffect(() => {
            if (connection) {
                connection.start()
                    .then(result => {
                        console.log('Connected!');
        
                        connection.on('RefreshCreatures', message => {
                            this.getCreatures();
                        });
                    })
                    .catch(e => console.log('Connection failed: ', e));
            }
        }, [connection]);
    
        const sendMessage = async (name, initiativeBonus, ac) => {
            const model = {
                Name: name,
                InitiativeBonus: initiativeBonus,
                AC: ac
            };

            try {
                await connection.send('AddCreature', model);
            }
            catch(e) {
                console.log(e);
            }
    
            /*if (connection.connectionStarted) {
            }
            else {
                alert('No connection to server yet.');
            }*/
        }

        if (error) {
          return <div>Błąd: {error.message}</div>;
        } else if (!isLoaded) {
          return <div>Ładowanie...</div>;
        } else {
          return (
              <>
                <ul>
                    {items.map(item => (
                    <li key={item.id}>
                        {item.name} {item.ac}
                    </li>
                    ))}
                </ul>
                <CreateCreature addCreature = {addCreature}/>
              </>
          );
        }
      }

};

export default CreaturePanel;