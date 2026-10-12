
# Part 3 - Test Strategy for an AI-Powered Security Alert Assistant

## 1. Testing Approach - What Does "Pass" Mean?

The assistant answers questions about security alerts by generating summaries and recommendations. Since the same input can produce different responses, I would not compare the output against an exact expected text.

My testing scope would focus on the accuracy of summaries, relevance and safety of recommendations, handling of incomplete data, and consistency across repeated responses. I would also include basic API reliability and security checks. I would not test the underlying threat detection engine, since the focus is on how the assistant interprets the provided alert data.

Because this is a security product, my main priority would be making sure the assistant provides correct information, stays focused on the alert, and does not mislead the user.

A response would pass if:

- **Accuracy:** Important details such as severity, threat type, affected devices, and actions taken match the alert data.
- **Grounding:** The assistant does not invent incidents, attack methods, or unsupported conclusions.
- **Completeness and relevance:** It answers the user's question and includes the critical information needed to understand the situation.
- **Safe recommendations:** Suggested actions are relevant and follow approved security guidance.
- **Uncertainty:** Missing or unclear information is acknowledged instead of guessed.
- **Response requirements:** It provides a summary and recommendations in the expected format, without disclosing unauthorized information.

I would distinguish between facts and recommendations. Suggesting a full security scan could be valid, but claiming that a scan was already completed would be incorrect if the alert does not confirm it.

I would repeat important scenarios, initially 10-20 times, to check consistency. Critical errors would not be accepted in any evaluated run, while minor quality differences could have agreed thresholds. Passing repeated runs would not guarantee that rare failures cannot occur.

## 2. Building a Repeatable Test Set

I would create a reusable, version-controlled dataset containing synthetic or anonymized security alerts, user questions, and expected validation rules.

Each test case would include:

- Input alert data and the user's question.
- Critical facts that must appear when relevant.
- Unsupported claims that must not appear.
- Acceptable recommendations and expected behavior.
- Risk level and pass/fail criteria.

I would cover the following scenarios:

| Scenario | What I Would Verify |
|---|---|
| High-severity threat | The risk is not minimized |
| Low-severity alert | No unnecessary escalation |
| Quarantined file | Quarantine is not confused with full resolution |
| Missing or conflicting data | The assistant does not invent missing details |
| Multiple alerts | No unsupported connections between incidents |
| Potential false positive | Suspicion is not presented as confirmed infection |
| Malicious text inside alert data | Instructions hidden in the data are ignored |

I would also test empty or malformed inputs, large alerts, and different user questions.

Another useful approach would be changing one input field at a time. For example, changing severity from Low to High should affect the urgency of recommendations. Removing an infection source should prevent the assistant from claiming that the source is known.

To make results comparable, I would record the input, generated response, model and prompt versions, configuration, and test results.

## 3. Detecting Hallucinations and Unsupported Information

One of the biggest risks is the assistant presenting assumptions as confirmed security facts.

**Example alert:**

- Device: PC-001
- Threat: Suspicious executable
- Severity: High
- Action taken: File quarantined
- Infection source: Unknown

**User question:** "Is my computer safe, and what should I do?"

**Acceptable response:** The assistant explains that the suspicious file was quarantined, clarifies that this does not guarantee the entire device is safe, and recommends further checks.

**Unacceptable response:** The assistant claims that the threat came from a phishing email, stole passwords, or that the computer is completely safe. These statements are not supported by the alert data.

To identify these problems, I would use several methods:

1. **Fact validation:** Compare devices, IP addresses, timestamps, severity, and status against the original alert.
2. **Claim validation:** Check whether factual statements are supported, contradicted, or unsupported. An AI evaluator could help flag issues, but its results would still need verification.
3. **Input variation:** Remove or change alert details and check that the assistant does not invent information or keep outdated conclusions.
4. **Repeated-run comparison:** Look for contradictory facts across responses generated from identical inputs, while allowing different wording.
5. **Manual review:** Investigate suspicious responses and review samples of passing results to catch problems missed by automation.

General security recommendations would be checked against approved guidance, since not every valid recommendation needs to appear directly in the alert.

## 4. Automation vs. Manual Verification

**I would automate** checks that are objective and repeatable:

- API responses, output structure, error handling, and response times.
- Critical alert facts, contradictions, and known unsupported claims.
- Repeated executions and regression testing after model or prompt changes.
- Negative scenarios involving missing data, invalid inputs, and prompt injection.

A smaller set of critical scenarios would run in CI, while broader evaluations would run periodically or before release.

**I would verify manually** areas requiring judgment and security knowledge:

- Whether recommendations are technically correct, useful, and safe.
- Whether the seriousness of the alert is communicated appropriately.
- Whether explanations are clear for the intended users.
- Whether uncertainty is handled responsibly.
- Unexpected scenarios discovered through exploratory testing.

For high-risk scenarios, I would involve security specialists to review recommendations and confirm expected behavior.

**Why this split?** Automation helps repeat tests, compare results, and detect recurring issues. Manual testing is still necessary because understanding the actual risk and deciding whether security advice is appropriate often requires human judgment.

## 5. Release Decision and Continuous Improvement

I would prioritize defects based on security impact rather than relying only on an overall pass percentage.

Critical issues, such as invented security incidents, unsafe recommendations, or incorrect claims that a threat has been resolved, would block release until investigated and addressed.

For each confirmed defect, I would save the input, question, AI response, configuration, and reason for failure. I would then add the scenario to the regression dataset to help prevent similar issues in future versions.

**My goal would not be to get identical responses every time, but to make sure that different responses remain correct, relevant, and safe. In a security product, a confident but incorrect answer can be more dangerous than an incomplete answer that clearly acknowledges uncertainty.**