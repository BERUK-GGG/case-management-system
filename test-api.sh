#!/bin/bash

echo "Testing RB Case Management API..."
echo "=================================="

# Wait for containers to be ready
echo "Waiting for containers to start..."
sleep 10

# Test API health
echo -e "\n1. Testing API connection..."
curl -s http://localhost:5001/api/customers || echo "API not ready yet"

echo -e "\n2. Testing Swagger UI..."
echo "Visit: http://localhost:5001/swagger"

echo -e "\n3. Available endpoints:"
echo "  GET  http://localhost:5001/api/customers"
echo "  POST http://localhost:5001/api/customers"
echo "  PUT  http://localhost:5001/api/customers/{id}"
echo "  GET  http://localhost:5001/api/visits"
echo "  POST http://localhost:5001/api/visits"
echo "  PUT  http://localhost:5001/api/visits/{id}"
echo "  GET  http://localhost:5001/api/journals"
echo "  POST http://localhost:5001/api/journals"
echo "  GET  http://localhost:5001/api/parts"
echo "  POST http://localhost:5001/api/parts"
echo "  GET  http://localhost:5001/api/mechanics"

echo -e "\n4. Sample POST request (create customer):"
cat << 'EOF'
curl -X POST http://localhost:5001/api/customers \
  -H "Content-Type: application/json" \
  -d '{
    "namn": "Test Customer",
    "personNr": 123456789,
    "address": "Test Address 123",
    "teleNr": 555123456,
    "epost": "test@example.com"
  }'
EOF

echo -e "\n\nContainers should be running. Check with: docker ps"
