/// <reference path="../typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />
export module Proofted {
	export class Configuration {
		public static Configure() {
			ko.validation.configure({
				registerExtenders: true,
				messagesOnModified: true,
				insertMessages: true,
				parseInputAttributes: true,
				messageTemplate: null,
				errorMessageClass: "help-inline",
				decorateElement: true,
				errorElementClass: 'error'
			});
		}
	}
}
