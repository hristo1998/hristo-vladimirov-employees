# hristo-vladimirov-employees
Coding Challenge Project Solution

## Getting Started

### Prerequisites

- [.NET 8 SDK]

---

### Running the MVC Application

1. **Clone the repository**
2. **Restore Dependecies**
3. **Run The App**
4. **Open in broweser**

http://localhost:5000


## CSV Format
The CSV file must contain the following columns:

EmpId	ProjectId	DateFrom	DateTo
1	10	2020-01-01	2020-01-15
2	10	2020-01-05	2020-01-20

- DateTo can be NULL for ongoing projects (will be replaced with DateTime.Now).

---

## Validation Rules
- File must not be empty
- File must be .csv
- File must not exceed 2MB
- Data rows must:
-- Contain all required columns
-- Have valid dates
-- Have positive employee and project IDs

---

## Example Usage
1. Upload CSV File
- Go to Home → Upload CSV
2. View Results
- Best pair collaboration
- Per-project collaboration details
- Total days worked together

---

## API Endpoint (for future SPA integration)
POST /api/collaboration/upload

- Body: multipart/form-data with file field
- Response: JSON with validation errors or calculation results


## Development Notes
- Services are registered via ServiceCollectionExtensions for clean DI configuration.
- File validation is reusable for both MVC and API controllers.
- Error passing is done via TempData extensions for cleaner redirect scenarios.
- API and MVC share business logic in the same core project.