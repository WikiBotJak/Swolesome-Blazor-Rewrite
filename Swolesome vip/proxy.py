#!/usr/bin/env python3
import json
from http.server import HTTPServer, BaseHTTPRequestHandler
from urllib import request as urllib_request, error as urllib_error
import sys

DEFAULT_API_BASE = "https://breach.vip/api/search"
PORT = int(sys.argv[1]) if len(sys.argv) > 1 else 8001


class ProxyHandler(BaseHTTPRequestHandler):
    def _send_cors_headers(self):
        self.send_header("Access-Control-Allow-Origin", "*")
        self.send_header("Access-Control-Allow-Methods", "POST, OPTIONS")
        self.send_header("Access-Control-Allow-Headers", "Content-Type")

    def do_OPTIONS(self):
        self.send_response(204)
        self._send_cors_headers()
        self.end_headers()

    def do_POST(self):
        content_length = int(self.headers.get("Content-Length", 0))
        post_data = self.rfile.read(content_length)

        try:
            # Parse the request body to extract target URL if provided
            body = json.loads(post_data.decode())
            target_url = body.pop("_target_url", DEFAULT_API_BASE)
            
            # Re-encode the body without the _target_url field
            cleaned_data = json.dumps(body).encode()

            req = urllib_request.Request(
                target_url,
                data=cleaned_data,
                method="POST",
                headers={"Content-Type": "application/json"},
            )

            with urllib_request.urlopen(req, timeout=15) as resp:
                status_code = resp.getcode()
                resp_headers = resp.info()
                resp_data = resp.read()

                self.send_response(status_code)
                self.send_header("Content-Type", resp_headers.get_content_type())
                self._send_cors_headers()
                self.end_headers()
                self.wfile.write(resp_data)

        except urllib_error.HTTPError as e:
            # Forward error code and body from the remote API
            self.send_response(e.code)
            self.send_header("Content-Type", "application/json")
            self._send_cors_headers()
            self.end_headers()
            try:
                body = e.read().decode()
                self.wfile.write(body.encode())
            except Exception:
                self.wfile.write(json.dumps({"error": str(e)}).encode())

        except Exception as e:
            # Handle general connection/timeouts
            self.send_response(502)
            self.send_header("Content-Type", "application/json")
            self._send_cors_headers()
            self.end_headers()
            self.wfile.write(json.dumps({"error": str(e)}).encode())


if __name__ == "__main__":
    print(f"Proxy running on http://localhost:{PORT}")
    print(f"Default API target: {DEFAULT_API_BASE}")
    print("Target URL can be overridden by sending '_target_url' in request body")
    HTTPServer(("", PORT), ProxyHandler).serve_forever()