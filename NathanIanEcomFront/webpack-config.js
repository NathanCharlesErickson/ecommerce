module.exports = {
    devtool: 'source-map',
    entry: ['regenerator-runtime/runtime', './App.tsx'],
    mode: "development",
    output: {
        filename: "./app-bundle.js"
    },
    resolve: {
        extensions: ['.Webpack.js', '.web.js', '.ts', '.js', '.jsx', '.tsx', '.css']
    },
    module: {
        rules: [
            { test: /\.css$/, use: ['style-loader', 'css-loader'] },
            { test: /\.(js|ts|jsx|tsx)$/, use: 'babel-loader'}
        ]
    }
}