import { RetryContext } from "@microsoft/signalr";

export default class CustomeRetryPolicy implements signalR.IRetryPolicy {
    maxRetryAttempts = 0;

    nextRetryDelayInMilliseconds(retryContext: RetryContext): number | null {
        console.info(`Retry: :${retryContext.retryReason}`);
        if(retryContext.previousRetryCount === 10) return null; // stop retry

        var nextRetry = retryContext.previousRetryCount * 1000 || 1000;
        console.log(`Retry in ${nextRetry} millisecond`);
        return nextRetry;
    }
}