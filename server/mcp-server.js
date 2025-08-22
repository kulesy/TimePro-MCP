#!/usr/bin/env node

const readline = require('readline');
const http = require('http');
const https = require('https');

const API_BASE_URL = 'http://localhost:5000/api';

// MCP Server implementation
class TimeProMcpServer {
  constructor() {
    this.rl = readline.createInterface({
      input: process.stdin,
      output: process.stdout,
      terminal: false
    });
    
    this.setupMessageHandling();
  }

  setupMessageHandling() {
    this.rl.on('line', (line) => {
      try {
        const message = JSON.parse(line);
        this.handleMessage(message);
      } catch (error) {
        console.error('Error parsing message:', error);
      }
    });
  }

  async handleMessage(message) {
    const { id, method, params } = message;

    try {
      let result;
      
      switch (method) {
        case 'initialize':
          result = {
            jsonrpc: '2.0',
            id,
            result: {
              protocolVersion: '2024-11-05',
              capabilities: {
                tools: {}
              },
              serverInfo: {
                name: 'timepro-mcp-server',
                version: '1.0.0'
              }
            }
          };
          break;

        case 'tools/list':
          result = {
            jsonrpc: '2.0',
            id,
            result: {
              tools: [
                {
                  name: 'timesheets/list',
                  description: 'Get all timesheets',
                  inputSchema: {
                    type: 'object',
                    properties: {},
                    required: []
                  }
                },
                {
                  name: 'timesheets/get',
                  description: 'Get a specific timesheet by ID',
                  inputSchema: {
                    type: 'object',
                    properties: {
                      id: {
                        type: 'integer',
                        description: 'Timesheet ID'
                      }
                    },
                    required: ['id']
                  }
                },
                {
                  name: 'timesheets/create',
                  description: 'Create a new timesheet',
                  inputSchema: {
                    type: 'object',
                    properties: {
                      date: {
                        type: 'string',
                        description: 'Date in YYYY-MM-DD format'
                      },
                      project: {
                        type: 'string',
                        description: 'Project name'
                      },
                      hours: {
                        type: 'number',
                        description: 'Hours worked'
                      },
                      details: {
                        type: 'string',
                        description: 'Work details'
                      },
                      status: {
                        type: 'string',
                        description: 'Status (Approved, Pending, Rejected)'
                      },
                      client: {
                        type: 'string',
                        description: 'Client name'
                      }
                    },
                    required: ['date', 'project', 'hours', 'details', 'status', 'client']
                  }
                },
                {
                  name: 'timesheets/update',
                  description: 'Update an existing timesheet',
                  inputSchema: {
                    type: 'object',
                    properties: {
                      id: {
                        type: 'integer',
                        description: 'Timesheet ID'
                      },
                      date: {
                        type: 'string',
                        description: 'Date in YYYY-MM-DD format'
                      },
                      project: {
                        type: 'string',
                        description: 'Project name'
                      },
                      hours: {
                        type: 'number',
                        description: 'Hours worked'
                      },
                      details: {
                        type: 'string',
                        description: 'Work details'
                      },
                      status: {
                        type: 'string',
                        description: 'Status (Approved, Pending, Rejected)'
                      },
                      client: {
                        type: 'string',
                        description: 'Client name'
                      }
                    },
                    required: ['id', 'date', 'project', 'hours', 'details', 'status', 'client']
                  }
                },
                {
                  name: 'timesheets/delete',
                  description: 'Delete a timesheet',
                  inputSchema: {
                    type: 'object',
                    properties: {
                      id: {
                        type: 'integer',
                        description: 'Timesheet ID'
                      }
                    },
                    required: ['id']
                  }
                }
              ]
            }
          };
          break;

        case 'tools/call':
          result = await this.handleToolCall(id, params);
          break;

        default:
          result = {
            jsonrpc: '2.0',
            id,
            error: {
              code: -32601,
              message: `Method not found: ${method}`
            }
          };
      }

      console.log(JSON.stringify(result));
    } catch (error) {
      console.log(JSON.stringify({
        jsonrpc: '2.0',
        id,
        error: {
          code: -32603,
          message: 'Internal error',
          data: error.message
        }
      }));
    }
  }

  async handleToolCall(id, params) {
    const { name, arguments: args } = params;
    
    try {
      const data = await this.makeHttpRequest(`${API_BASE_URL}/mcp/request`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          method: name,
          params: args
        })
      });
      
      return {
        jsonrpc: '2.0',
        id,
        result: {
          content: [
            {
              type: 'text',
              text: JSON.stringify(data, null, 2)
            }
          ]
        }
      };
    } catch (error) {
      return {
        jsonrpc: '2.0',
        id,
        error: {
          code: -32603,
          message: 'Tool call failed',
          data: error.message
        }
      };
    }
  }

  makeHttpRequest(url, options) {
    return new Promise((resolve, reject) => {
      const urlObj = new URL(url);
      const isHttps = urlObj.protocol === 'https:';
      const client = isHttps ? https : http;
      
      const requestOptions = {
        hostname: urlObj.hostname,
        port: urlObj.port,
        path: urlObj.pathname,
        method: options.method || 'GET',
        headers: options.headers || {}
      };

      const req = client.request(requestOptions, (res) => {
        let data = '';
        
        res.on('data', (chunk) => {
          data += chunk;
        });
        
        res.on('end', () => {
          try {
            const jsonData = JSON.parse(data);
            resolve(jsonData);
          } catch (error) {
            reject(new Error(`Failed to parse response: ${error.message}`));
          }
        });
      });

      req.on('error', (error) => {
        reject(error);
      });

      if (options.body) {
        req.write(options.body);
      }
      
      req.end();
    });
  }
}

// Start the server
const server = new TimeProMcpServer();
