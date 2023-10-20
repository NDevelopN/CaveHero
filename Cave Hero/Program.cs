using Cave;
using Server;

ThreadStart gameRef = new(CaveHero.Game);
Thread gameThread = new(gameRef);
gameThread.Start();

await Websocket.GetInstance().Start();