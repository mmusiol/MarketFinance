Top ProductApplicationService issues:
- single entry point witch sync code for all 3 microservices
- lack of logging and exception handling
- lack of comments when code is not written in self commenting manner 
	e.g.: returning ApplicationId from SubmitApplicationFor method is far away from obvious
- redundant code blocks 
- breaking the single responsibility rule: 
	service is responsible for orchestrating, data mapping, result handling, calling the services and that all in single method
- not named hardcoded const like "-1" in webservice result handling

Also:
- using built-in types for financial and time values without any context instead of creating value types
	e.g.: networth or amount without without declaring precision and currency, dates without time zone...