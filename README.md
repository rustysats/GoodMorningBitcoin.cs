# GoodMorningBitcoin Plugin

![Plugin Version](https://img.shields.io/badge/version-0.1.5-blue.svg)
![License](https://img.shields.io/badge/license-MIT-green.svg)
![Platform](https://img.shields.io/badge/platform-Oxide%20Plugin%20for%20Rust-orange.svg)

GoodMorningBitcoin is an Oxide plugin for Rust that displays "Now Playing" information from a configured radio API, providing players with the latest music being played on a designated radio station.

## Features
- Fetches and displays the "Now Playing" song from a specified radio API.
- Simple configuration for API endpoint setup.
- Caches responses to limit API calls and optimize performance.
- Easy-to-use command for players to see what's currently playing.

## Installation

1. **Download the Plugin**: Download `GoodMorningBitcoin.cs` and place it in your server's `oxide/plugins` directory.
2. **Reload the Plugin**: Use the command `oxide.reload GoodMorningBitcoin` in the server console or in-game to load the plugin.
3. **Configuration**: A default configuration file will be created in `oxide/config` on the first run.

## Configuration
The configuration file is simple and includes the following:

```json
{
    "ApiEndpoint": "https://radio.goodmorningbitcoin.com/api/nowplaying"
}
```

- **ApiEndpoint**: The URL of the radio API to fetch the "Now Playing" information.

## Commands

### `/goodmorningbitcoin`
- **Description**: Displays the current song playing on the configured radio station.
- **Usage**: Players can type `/goodmorningbitcoin` in the chat to receive the current "Now Playing" song information.

## Example Output
When a player types `/goodmorningbitcoin`, they may receive a response like:
```
Now Playing: Song Title - Artist Name
```

## Error Handling
If the plugin encounters an issue retrieving or processing the data, it will log the error and display a user-friendly message:
```
Unable to retrieve now playing information at the moment. Please try again later.
```

## Requirements
- **Oxide Mod**: Ensure your server has Oxide installed to run this plugin.
- **Rust Server**: A working Rust server with plugin capabilities.

## License
This plugin is open-source and licensed under the [MIT License](LICENSE).

## Contributing
Contributions are welcome! Feel free to submit issues or pull requests.
