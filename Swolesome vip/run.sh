#!/bin/bash

# Default frontend port
FRONTEND_PORT=8000

# Parse optional port arg
while [[ $# -gt 0 ]]; do
  case $1 in
    --port)
      FRONTEND_PORT="$2"
      shift 2
      ;;
    *)
      shift
      ;;
  esac
done

# Proxy port is frontend + 1
PROXY_PORT=$((FRONTEND_PORT + 1))

# --- Kill any old servers first ---
echo "Cleaning up old servers..."
pkill -f "python3 -m http.server" 2>/dev/null
pkill -f "proxy.py" 2>/dev/null

# --- Function to clean up on exit ---
cleanup() {
    echo
    echo "Stopping servers..."
    kill $FRONTEND_PID $PROXY_PID 2>/dev/null
    exit
}
trap cleanup SIGINT SIGTERM

# --- Start frontend server ---
echo "Starting frontend server on port $FRONTEND_PORT..."
python3 -m http.server "$FRONTEND_PORT" &
FRONTEND_PID=$!

# --- Start proxy server ---
echo "Starting proxy server on port $PROXY_PORT..."
python3 proxy.py "$PROXY_PORT" &
PROXY_PID=$!

# Wait a bit for servers to start
sleep 2

# --- Open default browser ---
if command -v xdg-open &> /dev/null; then
    xdg-open "http://localhost:$FRONTEND_PORT"
elif command -v open &> /dev/null; then
    open "http://localhost:$FRONTEND_PORT"
else
    echo "Open http://localhost:$FRONTEND_PORT manually in your browser."
fi

# --- Wait for both servers ---
wait $FRONTEND_PID $PROXY_PID
